using AutoMapper;
using Humanity.Application.Core.Services;
using Humanity.Application.Interfaces;
using Humanity.Application.Models.DTOs.Musteri;
using Humanity.Application.Models.Requests;
using Humanity.Application.Models.Responses;
using Humanity.Domain.Core.Repositories;
using Humanity.Domain.Core.Specifications;
using Humanity.Domain.Entities;
using System;
using System.Collections.Generic;
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
            var endeksler = await _unitOfWork.Repository<AboneAylikEndeks>().ListAsync(new BaseSpecification<AboneAylikEndeks>(a => a.AboneId == aboneId&& (a.Donem == donem || donem=="-1")));

            if (endeksler == null)
                throw new Exception("Endeks Bulunamadı");
            if (endeksler.Count > 0)
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
      .Select(g => new AboneAylikEndeks
      {
          AboneId = g.Key.AboneId,
          Donem = g.Key.Donem,
          Carpan = g.Key.Carpan,
          TotalTuketimCekis = g.Sum(x => x.CekisTuketim),
          TotalUretimVeris = g.Sum(x => x.Uretim),
          TotalReakIndVeris = g.Sum(x => x.VerisReaktifInduktif),
          TotalReakKapVeris = g.Sum(x => x.VerisReaktifKapasitif),
          TotalReakIndCekis = g.Sum(x => x.CekisReaktifInduktif),
          TotalReakKapCekis = g.Sum(x => x.CekisReaktifKapasitif),
          IsDeleted = false // Varsayılan olarak false olarak belirlenebilir
      })
      .First();

            var aylikEndeksSonuc = await _unitOfWork.Repository<AboneAylikEndeks>().AddAsync(aylikVeriler);

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
    }
}
