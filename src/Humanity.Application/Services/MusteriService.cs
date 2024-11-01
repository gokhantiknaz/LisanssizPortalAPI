using AutoMapper;
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

        public async Task<CustomResponseDto<Dto>> GetMusteriById(int id)
        {
            var musteri = await _unitOfWork.Repository<Musteri>().GetByIdAsync(id);

            if (musteri == null)
            {
                throw new NotFoundException   ($"{typeof(Musteri).Name} ({id}) not found");
            }

            if (musteri == null)
                throw new Exception("Müşteri Bulunamadı");

            musteri.MusteriIletisim = await GetMusteriIletisim(id);
            musteri.MusteriEntegrasyon = await GetMusteriEntegrasyon(id);

            var musteriDto = mapper.Map<Dto>(musteri);
            return CustomResponseDto<Dto>.Success(StatusCodes.Status200OK, musteriDto);

        }

        private async Task<MusteriIletisim> GetMusteriIletisim(int musteriId)
        {
            var spec = new BaseSpecification<MusteriIletisim>(x => x.MusteriId == musteriId);
            spec.AddInclude(a => a.Iletisim);

            var iletisim = await _unitOfWork.Repository<MusteriIletisim>().ListAsync(spec);

            return iletisim.FirstOrDefault();
        }

        public async Task<MusteriEntegrasyon> GetMusteriEntegrasyon(int musteriId)
        {
            var spec = new BaseSpecification<MusteriEntegrasyon>(x => x.MusteriId == musteriId);

            var entegre = await _unitOfWork.Repository<MusteriEntegrasyon>().ListAsync(spec);

            return entegre.FirstOrDefault();
        }

        public async Task<CustomResponseDto<Dto>> CreateMusteri(MusteriDTO req)
        {
            req.Durum = Status.Aktif;

            var musteri = mapper.Map<Musteri>(req);
            var musteriIletisim = mapper.Map<MusteriIletisim>(req.MusteriIletisim);
            var musteriEntergrasyon = mapper.Map<MusteriEntegrasyon>(req.MusteriEntegrasyon);

           // await _unitOfWork.Repository<Musteri>().AddAsync(musteri);

            musteriIletisim.Musteri = musteri;
            await _unitOfWork.Repository<MusteriIletisim>().AddAsync(musteriIletisim);

            musteriEntergrasyon.Musteri = musteri;
            await _unitOfWork.Repository<MusteriEntegrasyon>().AddAsync(musteriEntergrasyon);

            await _unitOfWork.SaveChangesAsync();
            var newDto = mapper.Map<Dto>(musteri);
            return CustomResponseDto<Dto>.Success(StatusCodes.Status200OK, newDto);
        }

        public async Task<CustomResponseDto<Dto>> Update(MusteriDTO req)
        {
            Musteri m = mapper.Map<Musteri>(req);

            _unitOfWork.Repository<Musteri>().Update(m);

            if (m.MusteriEntegrasyon != null && m.MusteriEntegrasyon.Id > 0)
            {
                var musterEntegrasyon = mapper.Map<MusteriEntegrasyon>(m.MusteriEntegrasyon);
                _unitOfWork.Repository<MusteriEntegrasyon>().Update(musterEntegrasyon);
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






        //public Task<GetAllActiveMusteriRes> MusteriyeBagliUreticiGetir(int musteriId)
        //{
        //    throw new NotImplementedException();
        //}

        //public async Task<CreateMusteriRes> CreateMusteri(CreateMusteriReq req)
        //{
        //    req.Durum = Status.Aktif.GetHashCode();
        //    Musteri m = mapper.Map<Musteri>(req);

        //    _ = await _unitOfWork.Repository<Musteri>().AddAsync(m);

        //    var miletisim = new Iletisim { Email = req.MusteriIletisim.Email ?? "", Adres = req.MusteriIletisim.Adres ?? "", CepTel = req.MusteriIletisim.CepTel ?? "", Ilid = req.MusteriIletisim.Ilid, Ilceid = req.MusteriIletisim.Ilceid };

        //    MusteriIletisim iletisim = new MusteriIletisim
        //    {
        //        Musteri = m,
        //        Iletisim = miletisim,
        //        IsDeleted = false,
        //        CreatedBy = Guid.Empty,
        //        CreatedOn = DateTime.UtcNow,
        //    };
        //    _ = await _unitOfWork.Repository<MusteriIletisim>().AddAsync(iletisim);


        //    var musterEntegrasyon = new MusteriEntegrasyon
        //    {
        //        ServisAdres = req.MusteriEntegrasyon.ServisAdres ?? "",
        //        KullaniciAdi = req.MusteriEntegrasyon.KullaniciAdi ?? "",
        //        Sifre = req.MusteriEntegrasyon.Sifre ?? "",
        //        ServisId = 1
        //    };
        //    musterEntegrasyon.Musteri = m;

        //    _ = await _unitOfWork.Repository<MusteriEntegrasyon>().AddAsync(musterEntegrasyon);

        //    //var listSubs= await _arilService.GetCustomerPortalSubscriptions();


        //    //foreach (var sub in listSubs.ResultList)
        //    //{
        //    //    //herbiri yeni abonedir.tüketici
        //    //    Abone a = new Abone()
        //    //    {

        //    //    };

        //    //    _ = await _unitOfWork.Repository<Abone>().AddAsync(a);
        //    //}


        //    try
        //    {
        //        await _unitOfWork.SaveChangesAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //    _loggerService.LogInfo("Müşteri Kaydedildi");

        //    return new CreateMusteriRes() { Data = new MusteriDTO(m) };

        //}




        //public async Task<GetMusteriRes> GetMusteriById(int id)
        //{
        //    var musteri = await _unitOfWork.Repository<Musteri>().GetByIdAsync(id);

        //    if (musteri == null)
        //        throw new Exception("Müşteri Bulunamadı");
        //    //throw NotFoundException("Cari");

        //    //iletisim bilgisi
        //    var iletisimDto = await GetMusteriIletisim(id);


        //    var data = mapper.Map<MusteriDTO>(musteri);
        //    var entegrasyon = await GetMusteriEntegrasyon(id);
        //    data.MusteriIletisim = new MusteriIletisimDTO(iletisimDto);

        //    data.MusteriEntegrasyon = entegrasyon;


        //    return new GetMusteriRes()
        //    {
        //        Data = data
        //    };
        //}

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

        //public Task<GetTuketiciListRes> MusteriyeBagliTuketicileriGetir(int aboneureticiId)
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
                    if(item.DefinitionType == 2)
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

               var allCustomers= await _unitOfWork.Repository<Abone>().ListAsync(new BaseSpecification<Abone>(x => x.MusteriId == musteriId));
                foreach (var item in allCustomers)
                {
                    string basDonem = DateTime.Now.Year.ToString() + "/" + "01";
                    string sonDonem = DateTime.Now.Year.ToString() + "/" + DateTime.Now.Month.ToString();
                    try
                    {
                        _ = await _arilService.GetEndOfMonthEndexes(item.Id, basDonem, sonDonem, true, item.SahisTip == SahisTip.Uretici);
                        // donem endeksini de getirelim
                        _ = await _arilService.GetCurrentEndexes(item.Id,true);
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
