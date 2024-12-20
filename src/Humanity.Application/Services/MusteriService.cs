﻿using AutoMapper;
using Humanity.Application.Core.Services;
using Humanity.Application.Interfaces;
using Humanity.Application.Models.DTOs;

using Humanity.Application.Models.DTOs.Musteri;
using Humanity.Domain.Exceptions;
using Humanity.Domain.Core.Models;
using Humanity.Domain.Core.Repositories;
using Humanity.Domain.Core.Specifications;
using Humanity.Domain.Entities;
using Microsoft.AspNetCore.Http;

using static Humanity.Domain.Enums.Enums;
using Humanity.Application.Models.Responses;
using Humanity.Domain.Specifications;
using Humanity.Application.Models.DTOs.ListDTOS;

namespace Humanity.Application.Services
{
    public class MusteriService<Entity, Dto> : IMusteriService<Entity, Dto> where Entity : BaseEntity where Dto : class
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggerService _loggerService;
        private readonly IArilService _arilService;
        private readonly IMapper mapper;

        public MusteriService(IUnitOfWork unitOfWork, ILoggerService loggerService, IMapper mapper, IArilService arilService)
        {
            _unitOfWork = unitOfWork;
            _loggerService = loggerService;
            this.mapper = mapper;
            _arilService = arilService;
        }

        public async Task<CustomResponseDto<IEnumerable<Dto>>> GetAllMusteri()
        {
            var activeSpec = new BaseSpecification<Musteri>(a => a.Durum == Domain.Enums.Enums.Status.Aktif);
            activeSpec.ApplyOrderByDescending(x => x.Id);
            //Convert.ToInt32("Texk");  // Hata kontrolü için ekledim kaldırılacak.
            var musteriler = await _unitOfWork.Repository<Musteri>().ListAsync(activeSpec);
            var dtos = mapper.Map<IEnumerable<Dto>>(musteriler);
            return CustomResponseDto<IEnumerable<Dto>>.Success(StatusCodes.Status200OK, dtos);
        }

        public async Task<CustomResponseDto<MusteriDTO>> GetMusteriById(int id)
        {
            var musteri = await _unitOfWork.Repository<Musteri>().GetByIdAsync(id);

            if (musteri == null)
            {
                throw new NotFoundException($"{typeof(Musteri).Name} ({id}) not found");
            }

            if (musteri == null)
                throw new Exception("Müşteri Bulunamadı");

            musteri.MusteriIletisim = await GetMusteriIletisim(id);

            var musteriDto = mapper.Map<MusteriDTO>(musteri);
            var entList = await GetMusteriEntegrasyon(id);
            musteriDto.MusteriEntegrasyon = mapper.Map<List<MusteriEntegrasyonDTO>>(entList);

            return CustomResponseDto<MusteriDTO>.Success(StatusCodes.Status200OK, musteriDto);

        }

        private async Task<MusteriIletisim> GetMusteriIletisim(int musteriId)
        {
            var spec = new BaseSpecification<MusteriIletisim>(x => x.MusteriId == musteriId);
            spec.AddInclude(a => a.Iletisim);

            var iletisim = await _unitOfWork.Repository<MusteriIletisim>().ListAsync(spec);

            return iletisim.FirstOrDefault();
        }

        public async Task<IEnumerable<MusteriEntegrasyon>> GetMusteriEntegrasyon(int musteriId)
        {
            var spec = new BaseSpecification<MusteriEntegrasyon>(x => x.MusteriId == musteriId);

            var entegre = await _unitOfWork.Repository<MusteriEntegrasyon>().ListAsync(spec);

            return entegre.ToList();
        }

        public async Task<CustomResponseDto<Dto>> CreateMusteri(MusteriDTO req)
        {
            req.Durum = Status.Aktif;

            var musteri = mapper.Map<Musteri>(req);
            var musteriIletisim = mapper.Map<MusteriIletisim>(req.MusteriIletisim);
            var musteriEntergrasyon = mapper.Map<List<MusteriEntegrasyon>>(req.MusteriEntegrasyon);

            // await _unitOfWork.Repository<Musteri>().AddAsync(musteri);

            musteriIletisim.Musteri = musteri;
            await _unitOfWork.Repository<MusteriIletisim>().AddAsync(musteriIletisim);

            foreach (var item in musteriEntergrasyon)
            {
                item.Musteri = musteri;
            }

            await _unitOfWork.Repository<MusteriEntegrasyon>().AddRandeAsync(musteriEntergrasyon);

            await _unitOfWork.SaveChangesAsync();
            var newDto = mapper.Map<Dto>(musteri);
            return CustomResponseDto<Dto>.Success(StatusCodes.Status200OK, newDto);
        }

        public async Task<CustomResponseDto<Dto>> Update(MusteriDTO req)
        {
            Musteri m = mapper.Map<Musteri>(req);
            m.Durum = Status.Aktif;
            _unitOfWork.Repository<Musteri>().Update(m);

            if (req.MusteriEntegrasyon != null && req.MusteriEntegrasyon.Count > 0)
            {
                foreach (var item in req.MusteriEntegrasyon)
                {
                    var musterEntegrasyon = mapper.Map<MusteriEntegrasyon>(item);
                    if (musterEntegrasyon.Id > 0)
                    {
                        _unitOfWork.Repository<MusteriEntegrasyon>().Update(musterEntegrasyon);
                    }
                    else
                    {
                        musterEntegrasyon.Musteri = m;
                        _ = await _unitOfWork.Repository<MusteriEntegrasyon>().AddAsync(musterEntegrasyon);
                    }
                }
            }

            if (m.MusteriIletisim != null && m.MusteriIletisim.IletisimId > 0)
            {
                var musteriIletisim = mapper.Map<MusteriIletisim>(m.MusteriIletisim);
                _unitOfWork.Repository<MusteriIletisim>().Update(musteriIletisim);
            }

            await _unitOfWork.SaveChangesAsync();

            var newDto = mapper.Map<Dto>(m);
            return CustomResponseDto<Dto>.Success(StatusCodes.Status200OK, newDto);
        }


        public async Task<CustomResponseDto<IEnumerable<AboneDTO>>> MusteriyeBagliTuketicileriGetir(int aboneid)
        {
            var tuketiciAboneSpec = AboneSpecifications.GetTuketiciByUretici(aboneid);
            tuketiciAboneSpec.AddInclude(a => a.Abone);
            tuketiciAboneSpec.AddInclude(a => a.Abone.Musteri);

            var ureticiAbone = await _unitOfWork.Repository<Abone>().GetByIdAsync(aboneid);

            var tuketiciler = await _unitOfWork.Repository<AboneTuketici>().ListAsync(tuketiciAboneSpec);

            var data = new List<AboneDTO>();
            foreach (var item in tuketiciler)
            {
                AboneDTO dTO = mapper.Map<AboneDTO>(item.Abone);
                data.Add(dTO);
            }

            return new CustomResponseDto<IEnumerable<AboneDTO>>() { Data = data };
        }

        public async Task<CustomResponseDto<IEnumerable<AboneDTO>>> MusteriyeBagliUreticiGetir(int musteriId)
        {
            var ureticiAboneSpec = AboneSpecifications.GetUreticiByMusteriId(musteriId);

            var ureticiAboneler = await _unitOfWork.Repository<AboneUretici>().ListAsync(ureticiAboneSpec);

            //var ureticiAboneler = await _unitOfWork.Repository<Abone>().ListAsync(ureticiAboneSpec);

            var data = new List<AboneDTO>();
            foreach (var item in ureticiAboneler)
            {
                AboneDTO dTO = mapper.Map<AboneDTO>(item.Abone);
                dTO.UreticiBilgileri = mapper.Map<UreticiDTO>(item);

                data.Add(dTO);
            }

            return new CustomResponseDto<IEnumerable<AboneDTO>>() { Data = data };

        }

        //public Task<CustomResponseDto<IEnumerable<Dto>>> GetAllMusteri()
        //{
        //    var activeSpec = new BaseSpecification<Musteri>(a => a.Durum == Domain.Enums.Enums.Status.Aktif);
        //    activeSpec.ApplyOrderByDescending(x => x.Id);

        //    var musteriler = await _unitOfWork.Repository<Musteri>().ListAsync(activeSpec);

        //    return new GetAllActiveMusteriRes()
        //    {
        //        Data = musteriler.Select(x => new MusteriDTO(x)).ToList()
        //    };
        //}

        //public Task<GetTuketiciListRes> GetBagimsizTuketiciler(int cariId)
        //{
        //    throw new NotImplementedException();
        //}



        //public async Task<MusteriEntegrasyonDTO> GetMusteriEntegrasyon(int musteriId)
        //{
        //    var spec = new BaseSpecification<MusteriEntegrasyon>(x => x.MusteriId == musteriId);

        //    var entegre = await _unitOfWork.Repository<MusteriEntegrasyon>().ListAsync(spec);

        //    return mapper.Map<MusteriEntegrasyonDTO>(entegre.FirstOrDefault());
        //}

        //private async Task<MusteriIletisim> GetMusteriIletisim(int musteriId)
        //{
        //    var spec = new BaseSpecification<MusteriIletisim>(x => x.MusteriId == musteriId);
        //    spec.AddInclude(a => a.Iletisim);

        //    var iletisim = await _unitOfWork.Repository<MusteriIletisim>().ListAsync(spec);

        //    return iletisim.FirstOrDefault();
        //}

        public async Task<bool> ArilBagliTuketiciKaydet(int musteriid)
        {

            var musteriler = await _arilService.GetCustomerPortalSubscriptions(musteriid);
            var retVal = new GetAboneResList();
            retVal.Data = new List<AboneDTO>();
            try
            {
                foreach (var item in musteriler.ResultList)
                {
                    var kayitli = await _unitOfWork.Repository<Abone>().ListAsync(new BaseSpecification<Abone>(a => a.SeriNo == item.SubscriptionSerno));
                    if (kayitli.Count > 0)
                        continue;

                    Abone a = mapper.Map<Abone>(item);
                    a.MusteriId = musteriid;
                    a.Durum = Status.Aktif;
                    a.DagitimFirmaId = item.DagitimFİrmaId;

                    if (item.DefinitionType == 15)
                    {
                        a.SahisTip = SahisTip.Uretici;
                        AboneUretici uretici = new AboneUretici()
                        {
                            CagrimektupTarihi = DateTime.UtcNow,
                            LisansBilgisi = LisansBilgisi.Lisanssız,
                            MahsupTipi = MahsupTipi.Aylık.GetHashCode(),
                            UretimBaslama = DateTime.UtcNow,
                            UretimSekli = UretimSekli.Ges
                        };
                        uretici.Abone = a;
                        _ = await _unitOfWork.Repository<AboneUretici>().AddAsync(uretici);
                    }
                    if (item.DefinitionType == 2)
                    {
                        a.SahisTip = SahisTip.DisTuketici;

                    }


                    AboneIletisim iletisim = new AboneIletisim() { CreatedBy = new Guid(), CreatedOn = DateTime.UtcNow, IsDeleted = false, Iletisim = new Iletisim() { Ilid = 6, Ilceid = 1130, Adres = item.Address, CepTel = "0535", Email = "a@a.com" } };
                    a.AboneIletisim = iletisim;

                    _ = await _unitOfWork.Repository<Abone>().AddAsync(a);

                    retVal.Data.Add(mapper.Map<AboneDTO>(a));
                }

                await _unitOfWork.SaveChangesAsync();


            }
            catch (Exception ex)
            {
                throw ex;
            }

            return true;
        }


        public async Task<bool> KaydedilenAboneEndeksleriAl(int musteriId)
        {

            try
            {
                // musteriye ait Aboneleri alalaım

                var allCustomers = await _unitOfWork.Repository<Abone>().ListAsync(new BaseSpecification<Abone>(x => x.MusteriId == musteriId));
                foreach (var item in allCustomers)
                {
                    string basDonem = (DateTime.Now.Year-1).ToString() + "/" + "01";
                    string sonDonem = DateTime.Now.Year.ToString() + "/" + DateTime.Now.Month.ToString();
                    try
                    {
                        _ = await _arilService.GetEndOfMonthEndexes(item.Id, basDonem, sonDonem, true, item.SahisTip == SahisTip.Uretici);
                        // donem endeksini de getirelim
                        _ = await _arilService.GetCurrentEndexes(item.Id, true);
                    }
                    catch (Exception er)
                    {
                        continue;
                    }

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return true;
        }
    }
}
