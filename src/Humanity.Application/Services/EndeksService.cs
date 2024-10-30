using AutoMapper;
using Humanity.Application.Core.Services;
using Humanity.Application.Interfaces;
using Humanity.Application.Models.DTOs;
using Humanity.Application.Models.DTOs.Musteri;
using Humanity.Application.Models.Requests;
using Humanity.Application.Models.Responses;
using Humanity.Domain.Core.Repositories;
using Humanity.Domain.Core.Specifications;
using Humanity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Humanity.Domain.Enums.Enums;

namespace Humanity.Application.Services
{
    public class EndeksService : IEndeksService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggerService _logService;

        private readonly IMapper mapper;

        public EndeksService(IUnitOfWork unitOfWork, ILoggerService loggerService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logService = loggerService;
            this.mapper = mapper;
        }

        public async Task<List<SaatlikEndeksRes>> GetAboneSaatlikEndeks(int aboneId, string donem)
        {
            var endeksler = await _unitOfWork.Repository<AboneSaatlikEndeks>().ListAsync(new BaseSpecification<AboneSaatlikEndeks>(a => a.AboneId == aboneId && a.Donem == donem));

            if (endeksler == null)
                throw new Exception("Endeks Bulunamadı");

            return mapper.Map<List<SaatlikEndeksRes>>(endeksler.ToList());
        }


        public async Task<List<AylikEndeksRes>> GetAboneDonemEndeks(int aboneId, string donem)
        {
            var endeksler = await _unitOfWork.Repository<AboneEndeks>().ListAsync(new BaseSpecification<AboneEndeks>(a => a.AboneId == aboneId && (a.EndexYear.ToString() == donem.Substring(4) && a.EndexMonth.ToString() == donem.Substring(4, 2)) || donem == "-1"));

            if (endeksler == null)
                throw new Exception("Endeks Bulunamadı");
            if (endeksler.Count() > 0)
            {
                return mapper.Map<List<AylikEndeksRes>>(endeksler);
            }
            else
                throw new Exception("Endeks Bulunamadı");
        }

        public async Task<SaatlikEndeksRes> Create(List<SaatlikEndeksRequest> req)
        {
            var saatlikEndkeksler = mapper.Map<List<AboneSaatlikEndeks>>(req);
            var saatlikEndeks = await _unitOfWork.Repository<AboneSaatlikEndeks>().AddRandeAsync(saatlikEndkeksler);

            var aylikVeriler = saatlikEndeks
      .GroupBy(v => new { v.AboneId, v.Donem, v.Carpan })
      .Select(g => new AboneEndeks
      {
          AboneId = g.Key.AboneId,
          EndexYear = Convert.ToInt32(g.Key.Donem.Substring(4)),
          EndexMonth = Convert.ToInt32(g.Key.Donem.Substring(4, 2)),
          T1Endex = 0,
          T2Endex = 0,
          T3Endex = 0,
          T4Endex = 0
      })
      .First();

            var aylikEndeksSonuc = await _unitOfWork.Repository<AboneEndeks>().AddAsync(aylikVeriler);

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            _logService.LogInfo("Firma Kaydedildi");


            return new SaatlikEndeksRes() { };
        }

        public async Task<bool> AylikEndeksKaydet(int aboneId, GetEndOfMonthEndexesResponse res)
        {

            // ilk data son okuma t1 t2 t3 tür o yüzden kümülatip gitnez. aktif ay endeksi
            if (res.ResultList.Count == 0)
                throw new Exception("Bu aya ait endeks bulunamadı");

            //aktif ay endeksini kaydederken. son endeksi kaydetsin. zaten son endeks gelir.
            var groupedResult = res.ResultList
          .Select(item => new
          {
              Item = item,
              Date = DateTime.ParseExact(item.EndexDate.ToString(), "yyyyMMddHHmmss", CultureInfo.InvariantCulture)
          })
          .GroupBy(x => new { Year = x.Date.Year, Month = x.Date.Month })
          .Select(g => g.OrderByDescending(x => x.Item.EndexDate).First().Item);

            foreach (var item in groupedResult)
            {
                var aylikEndeks = mapper.Map<AboneEndeks>(item);
                aylikEndeks.AboneId = aboneId;
                aylikEndeks.EndexMonth = aylikEndeks.EndexMonth == 0 ? DateTime.Now.Month : aylikEndeks.EndexMonth;
                aylikEndeks.EndexYear = aylikEndeks.EndexYear == 0 ? DateTime.Now.Year : aylikEndeks.EndexYear;

                //var oncekiAy = res.ResultList.FirstOrDefault(a => a.EndexMonth == aylikEndeks.EndexMonth - 1);

                //AboneEndeksPeriod periodGunduz = new AboneEndeksPeriod() { AboneEndeks = aylikEndeks, IlkEndeks = oncekiAy != null ? oncekiAy.T1Endex : 0, SonEndeks = item.T1Endex, EnerijCinsi = EnumEnerjiCinsi.Gunduz, EneriTur = EnunEneriTur.Aktif, EndeksDirection =  (EnumEndeksDirection)item.EndexType };
                //AboneEndeksPeriod periodPuant = new AboneEndeksPeriod() { AboneEndeks = aylikEndeks, IlkEndeks = oncekiAy != null ? oncekiAy.T2Endex : 0, SonEndeks = item.T2Endex, EnerijCinsi = EnumEnerjiCinsi.Gece, EneriTur = EnunEneriTur.Aktif, EndeksDirection = (EnumEndeksDirection)item.EndexType };
                //AboneEndeksPeriod periodGece = new AboneEndeksPeriod() { AboneEndeks = aylikEndeks, IlkEndeks = oncekiAy != null ? oncekiAy.T3Endex : 0, SonEndeks = item.T3Endex, EnerijCinsi = EnumEnerjiCinsi.Gece, EneriTur = EnunEneriTur.Aktif, EndeksDirection = (EnumEndeksDirection)item.EndexType };
                //AboneEndeksPeriod periodInduktif = new AboneEndeksPeriod() { AboneEndeks = aylikEndeks, IlkEndeks = oncekiAy != null ? oncekiAy.ReactiveInductive : 0, SonEndeks = item.ReactiveInductive, EnerijCinsi = EnumEnerjiCinsi.Tum, EneriTur = EnunEneriTur.Induktif, EndeksDirection = (EnumEndeksDirection)item.EndexType };
                //AboneEndeksPeriod periodKapasitif = new AboneEndeksPeriod() { AboneEndeks = aylikEndeks, IlkEndeks = oncekiAy != null ? oncekiAy.ReactiveCapasitive : 0, SonEndeks = item.ReactiveCapasitive, EnerijCinsi = EnumEnerjiCinsi.Tum, EneriTur = EnunEneriTur.Kapasitif, EndeksDirection = (EnumEndeksDirection)item.EndexType };

                // bu ay endeksi varsa once sil sonra ekle

                var spec = new BaseSpecification<AboneEndeks>(x => x.EndexYear == aylikEndeks.EndexYear && x.EndexMonth == aylikEndeks.EndexMonth && x.AboneId == aboneId && x.EndexType == item.EndexType);
                var dataExist = await _unitOfWork.Repository<AboneEndeks>().ListAsync(spec);
                foreach (var end in dataExist)
                {
                    _unitOfWork.Repository<AboneEndeks>().Delete(end);
                }

                _ = await _unitOfWork.Repository<AboneEndeks>().AddAsync(aylikEndeks);

     

                //_ = await _unitOfWork.Repository<AboneEndeksPeriod>().AddAsync(periodGunduz);
                //_ = await _unitOfWork.Repository<AboneEndeksPeriod>().AddAsync(periodPuant);
                //_ = await _unitOfWork.Repository<AboneEndeksPeriod>().AddAsync(periodGece);
                //_ = await _unitOfWork.Repository<AboneEndeksPeriod>().AddAsync(periodInduktif);
                //_ = await _unitOfWork.Repository<AboneEndeksPeriod>().AddAsync(periodKapasitif);

            }

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return true;
        }
    }
}
