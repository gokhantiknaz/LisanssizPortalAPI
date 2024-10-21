using Humanity.Application.Core.Services;
using Humanity.Application.Interfaces;
using Humanity.Application.Models.DTOs;
using Humanity.Application.Models.DTOs.ListDTOS;
using Humanity.Application.Models.Responses;
using Humanity.Application.Repositories;
using Humanity.Domain.Core.Repositories;
using Humanity.Domain.Core.Specifications;
using Humanity.Domain.Entities;
using Humanity.Domain.Specifications;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using static Humanity.Domain.Enums.Enums;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Humanity.Application.Models.DTOs.Musteri;

namespace Humanity.Application.Services
{
    public class AboneService : IAboneService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggerService _loggerService;
        private readonly IAboneRepository _Abonerep;
        public AboneService(IUnitOfWork unitOfWork, ILoggerService loggerService, IAboneRepository Abonerep)
        {
            _unitOfWork = unitOfWork;
            _loggerService = loggerService;
            _Abonerep = Abonerep;
        }

        public async Task<GetAboneRes> GetAboneById(int AboneId)
        {
            var AboneSpec = AboneSpecifications.GetAboneById(AboneId);

            var Abone = await _unitOfWork.Repository<Abone>().GetByIdAsync(AboneId);

            //abonesi
            var aboneSpec = new BaseSpecification<Abone>(a => a.Id == AboneId);
            var abone = await _unitOfWork.Repository<Abone>().ListAsync(aboneSpec);

            //uretici bilgisi varsa
            var ureticiSpec = new BaseSpecification<AboneUretici>(a => a.AboneId == abone.FirstOrDefault().Id);
            var aboneUretici = await _unitOfWork.Repository<AboneUretici>().ListAsync(ureticiSpec);


            //iletisim bilgisi
            var AboneIletisimDto = await GetAboneIletisim(AboneId);

            var data = new AboneDTO(Abone);

            data.AboneIletisim = AboneIletisimDto;
            data= new AboneDTO(abone.First());
            if (aboneUretici.Count > 0)
                data.UreticiBilgileri = new UreticiDTO(aboneUretici.FirstOrDefault(new AboneUretici()));

            //tujeticileri varsa
            if (aboneUretici.Count > 0 && abone.FirstOrDefault().SahisTip == SahisTip.Uretici)
            {
                var tuketicilerSpec = new BaseSpecification<AboneTuketici>(a => a.UreticiAboneId == aboneUretici.FirstOrDefault().Id);
                var tuketiciler = await _unitOfWork.Repository<AboneTuketici>().ListAsync(tuketicilerSpec);
                data.TuketiciList = new List<TuketiciListDTO>();
            }

            return new GetAboneRes()
            {
                Data = data
            };

        }

        public async Task<CreateAboneRes> CreateAbone(AboneDTO req)
        {
            var newiletisimAbone = new Iletisim { Email = req.AboneIletisim.Email ?? "", Adres = req.AboneIletisim.Adres ?? "", CepTel = req.AboneIletisim.CepTel ?? "", Ilid = req.AboneIletisim.Ilid, Ilceid = req.AboneIletisim.Ilceid };

            var abone = await _unitOfWork.Repository<Abone>().AddAsync(new Abone
            {
                Adi = req.Adi,
                Soyadi = req.Soyadi,
                CreatedOn = DateTime.UtcNow,
                Durum = Status.Aktif,
                SahisTip = req.SahisTip,
                SeriNo = req.SeriNo,
                SozlesmeGucu = req.SozlesmeGucu,
                IsDeleted = false,
                KuruluGuc = req.KuruluGuc,
                EtsoKodu = req.EtsoKodu,
                DagitimFirmaId = req.DagitimFirmaId,
                Agog = req.Agog,
                BaglantiGucu = req.BaglantiGucu,
                Tarife = req.Tarife,
                Terim = req.Terim,
                CreatedBy = Guid.Empty,
                Tckn = req.Tckn,
                Vkn = req.Vkn,
                Unvan = req.Unvan,
                OzelkodId1 = req.OzelkodId1,
                OzelkodId2 = req.OzelkodId2,
                OzelkodId3 = req.OzelkodId3,
              
                AboneIletisim = new AboneIletisim
                {
                    Iletisim = newiletisimAbone,
                    IsDeleted = false,
                    CreatedBy = Guid.Empty,
                    CreatedOn = DateTime.UtcNow,
                }
               
            });

            if (req.SahisTip == SahisTip.Uretici)
            {
                var aboneUretici = new AboneUretici()
                {
                    CagrimektupTarihi = req.UreticiBilgileri.CagrimektupTarihi,
                    LisansBilgisi = req.UreticiBilgileri.LisansBilgisi,
                    MahsupTipi = req.UreticiBilgileri.MahsupTipi,
                    UretimBaslama = req.UreticiBilgileri.UretimBaslama,
                    UretimSekli = req.UreticiBilgileri.UretimSekli
                };

                _ = await _unitOfWork.Repository<AboneUretici>().AddAsync(aboneUretici);

                // tüketici listesi.

                if (req.TuketiciList.Count > 0)
                {
                    foreach (var item in req.TuketiciList)
                    {
                        var tuketici = new AboneTuketici()
                        {
                            IsDeleted = false,
                            BaslamaZamani = DateTime.UtcNow,
                            Durum = Status.Aktif,
                            UreticiAbone = abone,
                            AboneId = item.AboneId,
                        };

                        _ = await _unitOfWork.Repository<AboneTuketici>().AddAsync(tuketici);
                    }
                }
            }

            if (req.SayacList.Count > 0)
            {
                foreach (var item in req.SayacList)
                {
                    var sayacBilgi = new AboneSayac()
                    {
                        Abone = abone,
                        FazAdedi = item.FazAdedi,
                        Marka = item.Marka,
                        SayacNo = item.SayacNo
                    };

                    _ = await _unitOfWork.Repository<AboneSayac>().AddAsync(sayacBilgi);
                }
            }

            //var newiletisimAbone = new Iletisim { CepTel = "", Email = "", Adres = "", Ilid = req.Abone.AboneIletisim.Ilid, Ilceid = req.Abone.AboneIletisim.Ilceid };

            //AboneIletisim iletisimabone = new AboneIletisim
            //{
            //    Abone = Abone.Abone,
            //    Iletisim = newiletisimAbone,
            //    CreatedBy = Guid.Empty,
            //    CreatedOn = DateTime.UtcNow,
            //    IsDeleted = false
            //};

            //_ = _unitOfWork.Repository<AboneIletisim>().AddAsync(iletisimabone);

            await _unitOfWork.SaveChangesAsync();

            _loggerService.LogInfo("Yeni Müşteri Eklendi");

            return new CreateAboneRes() { Data = new AboneDTO(abone) };
        }


        public async Task<GetAboneRes> Update(AboneDTO req)
        {
            var m = await _unitOfWork.Repository<Abone>().GetByIdAsync(req.Id);

            m.Adi = req.Adi;
            m.Soyadi = req.Soyadi;
            m.CreatedOn = DateTime.UtcNow;
            m.Durum = Status.Aktif;
            m.IsDeleted = false;
            m.Tckn = req.Tckn;
            m.Vkn = req.Vkn;
            m.Unvan = req.Unvan;
            m.OzelkodId1 = req.OzelkodId1;
            m.OzelkodId2 = req.OzelkodId2;
            m.OzelkodId3 = req.OzelkodId3;

            _unitOfWork.Repository<Abone>().Update(m);

            Iletisim Aboneiletisim;
            if (req.AboneIletisim.IletisimId > 0)
            {
                Aboneiletisim = await _unitOfWork.Repository<Iletisim>().GetByIdAsync(req.AboneIletisim.IletisimId);

                Aboneiletisim.Adres = req.AboneIletisim.Adres ?? "";
                Aboneiletisim.Ilid = req.AboneIletisim.Ilid.HasValue ? req.AboneIletisim.Ilid.Value : null;
                Aboneiletisim.Ilceid = req.AboneIletisim.Ilceid.HasValue ? req.AboneIletisim.Ilceid.Value : null;
                Aboneiletisim.CepTel = req.AboneIletisim.CepTel ?? "";
                Aboneiletisim.Email = req.AboneIletisim.Email ?? "";
                _unitOfWork.Repository<Iletisim>().Update(Aboneiletisim);
            }
            else
            {
                var newIletisim = new Iletisim { Email = req.AboneIletisim.Email ?? "", Adres = req.AboneIletisim.Adres ?? "", CepTel = req.AboneIletisim.CepTel ?? "", Ilid = req.AboneIletisim.Ilid, Ilceid = req.AboneIletisim.Ilceid };

                AboneIletisim iletisimNEw = new AboneIletisim
                {
                    Abone = m,
                    Iletisim = newIletisim,
                    IsDeleted = false,
                    CreatedBy = Guid.Empty,
                    CreatedOn = DateTime.UtcNow,
                };

                _ = await _unitOfWork.Repository<AboneIletisim>().AddAsync(iletisimNEw);
            }

            var abone = await _unitOfWork.Repository<Abone>().GetByIdAsync(req.Id);
            abone.SahisTip = req.SahisTip;
            abone.SeriNo = req.SeriNo;
            abone.SozlesmeGucu = req.SozlesmeGucu;
            abone.IsDeleted = false;
            abone.KuruluGuc = req.KuruluGuc;
            abone.EtsoKodu = req.EtsoKodu;
            abone.DagitimFirmaId = req.DagitimFirmaId;
            abone.Agog = req.Agog;
            abone.BaglantiGucu = req.BaglantiGucu;
            abone.Tarife = req.Tarife;
            abone.Terim = req.Terim;
            abone.LastModifiedBy = Guid.Empty;
            abone.LastModifiedOn = DateTime.UtcNow;

            if (req.SahisTip == SahisTip.Uretici)
            {
                var aboneUretici = await _unitOfWork.Repository<AboneUretici>().GetByIdAsync(req.UreticiBilgileri.Id);

                aboneUretici.CagrimektupTarihi = req.UreticiBilgileri.CagrimektupTarihi;
                aboneUretici.LisansBilgisi = req.UreticiBilgileri.LisansBilgisi;
                aboneUretici.MahsupTipi = req.UreticiBilgileri.MahsupTipi;
                aboneUretici.UretimBaslama = req.UreticiBilgileri.UretimBaslama;
                aboneUretici.UretimSekli = req.UreticiBilgileri.UretimSekli;

                _unitOfWork.Repository<AboneUretici>().Update(aboneUretici);

                // tüketici listesi.

                if (req.TuketiciList.Count > 0)
                {
                    var kayitlilar = await AboneyeBagliTuketicileriGetir(m.Id);
                    foreach (var item in kayitlilar.Data)
                    {
                        if(!req.TuketiciList.Any(a => a.AboneId == item.AboneId)) // silinmiş
                        {
                            var tuketici = await _unitOfWork.Repository<AboneTuketici>().ListAsync(new BaseSpecification<AboneTuketici>(a => a.AboneId == item.AboneId));
                            tuketici.First().IsDeleted= true;
                            _unitOfWork.Repository<AboneTuketici>().Update(tuketici.First());
                        }
                    }

                    foreach (TuketiciListDTO tuketici in req.TuketiciList)
                    {
                        //delete i de yapalım
                        if (kayitlilar.Data.Count(a=>a.AboneId== tuketici.AboneId && abone.Id== tuketici.UreticiAboneId)>0)
                        {
                            var tuketiciTekrar = await _unitOfWork.Repository<AboneTuketici>().ListAsync(new BaseSpecification<AboneTuketici>(a => a.AboneId == tuketici.AboneId));
                            tuketiciTekrar.First().IsDeleted = false;
                            _unitOfWork.Repository<AboneTuketici>().Update(tuketiciTekrar.First());
                        }
                        else
                        {
                            var yenituketici = new AboneTuketici()
                            {
                                IsDeleted = false,
                                BaslamaZamani = DateTime.UtcNow,
                                Durum = Status.Aktif,
                                AboneId = tuketici.AboneId,
                                UreticiAboneId=abone.Id
                            };

                            _ = await _unitOfWork.Repository<AboneTuketici>().AddAsync(yenituketici);
                        }
                    }
                }
            }

            if (req.SayacList.Count > 0)
            {
                foreach (var item in req.SayacList)
                {
                    var sayacBilgi = new AboneSayac()
                    {
                        Abone = abone,
                        FazAdedi = item.FazAdedi,
                        Marka = item.Marka,
                        SayacNo = item.SayacNo
                    };

                    _ = await _unitOfWork.Repository<AboneSayac>().AddAsync(sayacBilgi);
                }
            }


            await _unitOfWork.SaveChangesAsync();

            _loggerService.LogInfo("Yeni Müşteri Eklendi");

            return new GetAboneRes() { Data = new AboneDTO(m) };
        }

        //public async Task<GetAllActiveAboneTableRes> GetAllAbone()
        //{
        //    var activeUsersSpec = AboneSpecifications.GetAllActiveUsersSpec();
        //    activeUsersSpec.ApplyOrderByDescending(x => x.Id);

        //    var Aboneler = await _unitOfWork.Repository<Abone>().ListAsync(activeUsersSpec);

        //    return new GetAllActiveAboneTableRes()
        //    {
        //        Data = Aboneler.Select(x => new AboneTableDTO(x)).ToList()
        //    };
        //}

        public async Task<AboneDTO> GetAboneUreticiAbone(int aboneid)
        {
            var activeSpec = new BaseSpecification<Abone>(a => a.Id == aboneid && a.SahisTip == SahisTip.Uretici);
            var aboneler = await _unitOfWork.Repository<Abone>().ListAsync(activeSpec);

            var data = aboneler.Select(x => new AboneDTO(x)).FirstOrDefault();
            return data;
        }

        public async Task<AboneIletisimDTO> GetAboneIletisim(int AboneId)
        {
            var activeSpec = new BaseSpecification<AboneIletisim>(a => a.AboneId == AboneId);
            activeSpec.AddInclude(a => a.Iletisim);
            var AbonelerIletisimler = await _unitOfWork.Repository<AboneIletisim>().ListAsync(activeSpec);

            if (AbonelerIletisimler.Count == 0)
                return new AboneIletisimDTO() { };
            return new AboneIletisimDTO()
            {
                Adres = AbonelerIletisimler[0].Iletisim.Adres,
                CepTel = AbonelerIletisimler[0].Iletisim.CepTel,
                Email = AbonelerIletisimler[0].Iletisim.Email,
                Ilceid = AbonelerIletisimler[0].Iletisim.Ilceid,
                Ilid = AbonelerIletisimler[0].Iletisim.Ilid,
                IletisimId = (int)AbonelerIletisimler[0].IletisimId
            };
        }

        public async Task<GetTuketiciListRes> AboneyeBagliTuketicileriGetir(int ureticiAboneId)
        {
            var tuketiciAboneSpec = AboneSpecifications.GetTuketiciByUretici(ureticiAboneId);
            tuketiciAboneSpec.AddInclude(a => a.Abone);
            tuketiciAboneSpec.AddInclude(a=>a.Abone);
            tuketiciAboneSpec.AddInclude(a => a.Abone.AboneIletisim);
            tuketiciAboneSpec.AddInclude(a => a.Abone.AboneIletisim.Iletisim);

            var ureticiAbone = await _unitOfWork.Repository<Abone>().GetByIdAsync(ureticiAboneId);

            var activeSpec = new BaseSpecification<Abone>(a => a.Id == ureticiAboneId&& a.SahisTip == SahisTip.Uretici);
          

            var Aboneler = await _unitOfWork.Repository<AboneTuketici>().ListAsync(tuketiciAboneSpec);

            var data = Aboneler.Select(x => new TuketiciTableDTO(x) { UreticiAboneId=ureticiAboneId,UreticiAbone=new AboneDTO(ureticiAbone) {  } }).ToList();
            return new GetTuketiciListRes()
            {
                Data = data
            };
        }

        public async Task<GetTuketiciListRes> GetBagimsizTuketiciler(int musteriId)
        {
            var AboneDtos = _Abonerep.GetBagimsizTuketiciler(musteriId).ToList();

            return new GetTuketiciListRes()
            {
                Data = AboneDtos
            };
        }

        public Task<List<GetAboneRes>> GetMusteriAboneler(int musteriid)
        {
            throw new NotImplementedException();
        }

        public Task<List<GetAboneRes>> GetFirmaAboneler()
        {
            throw new NotImplementedException();
        }
    }
}
