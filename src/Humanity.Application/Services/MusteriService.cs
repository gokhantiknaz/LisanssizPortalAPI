using Humanity.Application.Core.Services;
using Humanity.Application.Interfaces;
using Humanity.Application.Models.DTOs;
using Humanity.Application.Models.DTOs.Musteri;
using Humanity.Application.Models.Requests.Musteri;
using Humanity.Application.Models.Responses;
using Humanity.Application.Models.Responses.Musteri;
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
using static Humanity.Domain.Enums.Enums;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Humanity.Application.Services
{
    public class MusteriService : IMusteriService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggerService _loggerService;
        private readonly IMusteriRepository _musterirep;

        public MusteriService(IUnitOfWork unitOfWork, ILoggerService loggerService, IMusteriRepository musterirep)
        {
            _unitOfWork = unitOfWork;
            _loggerService = loggerService;
            _musterirep = musterirep;

        }

        public async Task<GetMusteriRes> GetMusteriById(int musteriId)
        {
            var musteriSpec = MusteriSpecifications.GetMusteriById(musteriId);

            var musteri = await _unitOfWork.Repository<Musteri>().ListAsync(musteriSpec);

            //abonesi
            var aboneSpec = new BaseSpecification<Abone>(a => a.MusteriId == musteriId);
            var abone = await _unitOfWork.Repository<Abone>().ListAsync(aboneSpec);

            //uretici bilgisi varsa
            var ureticiSpec = new BaseSpecification<AboneUretici>(a => a.AboneId == abone.FirstOrDefault().Id);
            var aboneUretici = await _unitOfWork.Repository<AboneUretici>().ListAsync(ureticiSpec);



            //iletisim bilgisi
            var musteriIletisimDto = await GetMusteriIletisim(musteriId);

            var data = new MusteriDTO(musteri.FirstOrDefault());

            data.MusteriIletisim = musteriIletisimDto;
            data.Abone = new AboneDTO(abone.First());
            if (aboneUretici.Count > 0)
                data.Abone.UreticiBilgileri = new UreticiDTO(aboneUretici.FirstOrDefault(new AboneUretici()));

            //tujeticileri varsa
            if (aboneUretici.Count > 0 && abone.FirstOrDefault().SahisTip == SahisTip.Uretici)
            {
                var tuketicilerSpec = new BaseSpecification<AboneTuketici>(a => a.UreticiAboneId == aboneUretici.FirstOrDefault().Id);
                var tuketiciler = await _unitOfWork.Repository<AboneTuketici>().ListAsync(tuketicilerSpec);
                data.TuketiciList = new List<TuketiciListDTO>();
            }

            return new GetMusteriRes()
            {
                Data = data
            };

        }

        public async Task<CreateMusteriRes> CreateMusteri(CreateMusteriReq req)
        {

            var newiletisimMusteri = new Iletisim { Email = req.MusteriIletisim.Email ?? "", Adres = req.MusteriIletisim.Adres ?? "", CepTel = req.MusteriIletisim.CepTel ?? "", Ilid = req.MusteriIletisim.Ilid, Ilceid = req.MusteriIletisim.Ilceid };

            var abone = new Abone
            {
                SahisTip = req.Abone.SahisTip,
                SeriNo = req.Abone.SeriNo,
                SozlesmeGucu = req.Abone.SozlesmeGucu,
                IsDeleted = false,
                KuruluGuc = req.Abone.KuruluGuc,
                EtsoKodu = req.Abone.EtsoKodu,
                DagitimFirmaId = req.Abone.DagitimFirmaId,
                Agog = req.Abone.Agog,
                BaglantiGucu = req.Abone.BaglantiGucu,
                Tarife = req.Abone.Tarife,
                Terim = req.Abone.Terim,
                CreatedBy = Guid.Empty,
                CreatedOn = DateTime.UtcNow,
            };


            var musteri = await _unitOfWork.Repository<Musteri>().AddAsync(new Musteri
            {
                Adi = req.Adi,
                Soyadi = req.Soyadi,
                CreatedOn = DateTime.UtcNow,
                Durum = Status.Aktif,
                GercekTuzel = (Domain.Enums.Enums.GercekTuzel)req.GercekTuzel,
                IsDeleted = false,
                Tckn = req.Tckn,
                Vkn = req.Vkn,
                Unvan = req.Unvan,
                OzelkodId1 = req.OzelkodId1,
                OzelkodId2 = req.OzelkodId2,
                OzelkodId3 = req.OzelkodId3,
                CariKartId = req.CariKartId,
                MusteriIletisim = new MusteriIletisim
                {
                    Iletisim = newiletisimMusteri,
                    IsDeleted = false,
                    CreatedBy = Guid.Empty,
                    CreatedOn = DateTime.UtcNow,
                },
                Abone = abone
            });

            if (req.Abone.SahisTip == SahisTip.Uretici)
            {
                var aboneUretici = new AboneUretici()
                {
                    Abone = musteri.Abone,
                    CagrimektupTarihi = req.Abone.UreticiBilgileri.CagrimektupTarihi,
                    LisansBilgisi = req.Abone.UreticiBilgileri.LisansBilgisi,
                    MahsupTipi = req.Abone.UreticiBilgileri.MahsupTipi,
                    UretimBaslama = req.Abone.UreticiBilgileri.UretimBaslama,
                    UretimSekli = req.Abone.UreticiBilgileri.UretimSekli
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
            //    Abone = musteri.Abone,
            //    Iletisim = newiletisimAbone,
            //    CreatedBy = Guid.Empty,
            //    CreatedOn = DateTime.UtcNow,
            //    IsDeleted = false
            //};

            //_ = _unitOfWork.Repository<AboneIletisim>().AddAsync(iletisimabone);

            await _unitOfWork.SaveChangesAsync();

            _loggerService.LogInfo("Yeni Müşteri Eklendi");

            return new CreateMusteriRes() { Data = new MusteriDTO(this, musteri) };
        }


        public async Task<GetMusteriRes> Update(UpdateMusteriReq req)
        {
            var m = await _unitOfWork.Repository<Musteri>().GetByIdAsync(req.Id);

            m.Adi = req.Adi;
            m.Soyadi = req.Soyadi;
            m.CreatedOn = DateTime.UtcNow;
            m.Durum = Status.Aktif;
            m.GercekTuzel = (Domain.Enums.Enums.GercekTuzel)req.GercekTuzel;
            m.IsDeleted = false;
            m.Tckn = req.Tckn;
            m.Vkn = req.Vkn;
            m.Unvan = req.Unvan;
            m.OzelkodId1 = req.OzelkodId1;
            m.OzelkodId2 = req.OzelkodId2;
            m.OzelkodId3 = req.OzelkodId3;
            m.CariKartId = req.CariKartId;

            _unitOfWork.Repository<Musteri>().Update(m);

            //var musteriiletisim = await _unitOfWork.Repository<MusteriIletisim>().FirstOrDefaultAsync(new BaseSpecification<MusteriIletisim>(x => x.IletisimId == req.MusteriIletisim.IletisimId && x.MusteriId == req.MusteriIletisim.MusteriId));

            //var iletisim = await _unitOfWork.Repository<Iletisim>().FirstOrDefaultAsync(new BaseSpecification<Iletisim>(x => x.Id == musteriiletisim.IletisimId));

            //iletisim.Email = req.MusteriIletisim.Email ?? "";
            //iletisim.Adres = req.MusteriIletisim.Adres ?? "";
            //iletisim.CepTel = req.MusteriIletisim.CepTel ?? "";
            //iletisim.Ilid = req.MusteriIletisim.Ilid;
            //iletisim.Ilceid = req.MusteriIletisim.Ilceid;

            Iletisim musteriiletisim;
            if (req.MusteriIletisim.IletisimId > 0)
            {
                musteriiletisim = await _unitOfWork.Repository<Iletisim>().GetByIdAsync(req.MusteriIletisim.IletisimId);

                musteriiletisim.Adres = req.MusteriIletisim.Adres ?? "";
                musteriiletisim.Ilid = req.MusteriIletisim.Ilid.HasValue ? req.MusteriIletisim.Ilid.Value : null;
                musteriiletisim.Ilceid = req.MusteriIletisim.Ilceid.HasValue ? req.MusteriIletisim.Ilceid.Value : null;
                musteriiletisim.CepTel = req.MusteriIletisim.CepTel ?? "";
                musteriiletisim.Email = req.MusteriIletisim.Email ?? "";
                _unitOfWork.Repository<Iletisim>().Update(musteriiletisim);
            }
            else
            {
                var newIletisim = new Iletisim { Email = req.MusteriIletisim.Email ?? "", Adres = req.MusteriIletisim.Adres ?? "", CepTel = req.MusteriIletisim.CepTel ?? "", Ilid = req.MusteriIletisim.Ilid, Ilceid = req.MusteriIletisim.Ilceid };

                MusteriIletisim iletisimNEw = new MusteriIletisim
                {
                    Musteri = m,
                    Iletisim = newIletisim,
                    IsDeleted = false,
                    CreatedBy = Guid.Empty,
                    CreatedOn = DateTime.UtcNow,
                };

                _ = await _unitOfWork.Repository<MusteriIletisim>().AddAsync(iletisimNEw);
            }


            var abone = new Abone();
            abone.SahisTip = req.Abone.SahisTip;
            abone.SeriNo = req.Abone.SeriNo;
            abone.SozlesmeGucu = req.Abone.SozlesmeGucu;
            abone.IsDeleted = false;
            abone.KuruluGuc = req.Abone.KuruluGuc;
            abone.EtsoKodu = req.Abone.EtsoKodu;
            abone.DagitimFirmaId = req.Abone.DagitimFirmaId;
            abone.Agog = req.Abone.Agog;
            abone.BaglantiGucu = req.Abone.BaglantiGucu;
            abone.Tarife = req.Abone.Tarife;
            abone.Terim = req.Abone.Terim;
            abone.LastModifiedBy = Guid.Empty;
            abone.LastModifiedOn = DateTime.UtcNow;




            if (req.Abone.SahisTip == SahisTip.Uretici)
            {
                var aboneUretici = await _unitOfWork.Repository<AboneUretici>().GetByIdAsync(req.Abone.UreticiBilgileri.Id);

                aboneUretici.CagrimektupTarihi = req.Abone.UreticiBilgileri.CagrimektupTarihi;
                aboneUretici.LisansBilgisi = req.Abone.UreticiBilgileri.LisansBilgisi;
                aboneUretici.MahsupTipi = req.Abone.UreticiBilgileri.MahsupTipi;
                aboneUretici.UretimBaslama = req.Abone.UreticiBilgileri.UretimBaslama;
                aboneUretici.UretimSekli = req.Abone.UreticiBilgileri.UretimSekli;

                _unitOfWork.Repository<AboneUretici>().Update(aboneUretici);

                // tüketici listesi.

                if (req.TuketiciList.Count > 0)
                {
                    var kayitlilar = await MusteriyeBagliTuketicileriGetir(m.Id);
                    foreach (var item in kayitlilar.Data)
                    {
                        if(!req.TuketiciList.Any(a => a.AboneId == item.Abone.Id)) // silinmiş
                        {
                            
                        }
                    }

                    foreach (var item in req.TuketiciList)
                    {
                        //delete i de yapalım
                        if (item.Id > 0)
                        {

                        }
                        else
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

            return new GetMusteriRes() { Data = new MusteriDTO(this, m) };
        }

        public async Task<GetAllActiveMusteriRes> GetAllMusteri()
        {
            var activeUsersSpec = MusteriSpecifications.GetAllActiveUsersSpec();
            activeUsersSpec.ApplyOrderByDescending(x => x.Id);

            var musteriler = await _unitOfWork.Repository<Musteri>().ListAsync(activeUsersSpec);

            return new GetAllActiveMusteriRes()
            {
                Data = musteriler.Select(x => new MusteriDTO(this, x)).ToList()
            };
        }

        public async Task<AboneDTO> GetMusteriUreticiAbone(int musteriId)
        {
            var activeSpec = new BaseSpecification<Abone>(a => a.MusteriId == musteriId && a.SahisTip == SahisTip.Uretici);
            var aboneler = await _unitOfWork.Repository<Abone>().ListAsync(activeSpec);

            var data = aboneler.Select(x => new AboneDTO(x)).FirstOrDefault();
            return data;
        }

        public async Task<MusteriIletisimDTO> GetMusteriIletisim(int musteriId)
        {
            var activeSpec = new BaseSpecification<MusteriIletisim>(a => a.MusteriId == musteriId);
            activeSpec.AddInclude(a => a.Iletisim);
            var musterilerIletisimler = await _unitOfWork.Repository<MusteriIletisim>().ListAsync(activeSpec);

            if (musterilerIletisimler.Count == 0)
                return new MusteriIletisimDTO() { };
            return new MusteriIletisimDTO()
            {
                Adres = musterilerIletisimler[0].Iletisim.Adres,
                CepTel = musterilerIletisimler[0].Iletisim.CepTel,
                Email = musterilerIletisimler[0].Iletisim.Email,
                Ilceid = musterilerIletisimler[0].Iletisim.Ilceid,
                Ilid = musterilerIletisimler[0].Iletisim.Ilid,
                IletisimId = musterilerIletisimler[0].IletisimId
            };
        }

        public async Task<ValidateMusteriRes> ValidateMusteri(ValidateMusteriReq req)
        {
            throw new NotImplementedException();
        }

        public async Task<GetAllActiveMusteriRes> MusteriyeBagliTuketicileriGetir(int musteriid)
        {
            var ureticiAboneSpec = MusteriSpecifications.GetUreticiByMusteri(musteriid);

            var musteriler = await _unitOfWork.Repository<AboneUretici>().ListAsync(ureticiAboneSpec);

            var data = musteriler.Select(x => new MusteriDTO(this, x.Abone.Musteri)).ToList();
            return new GetAllActiveMusteriRes()
            {
                Data = data
            };
        }

        public async Task<GetAllActiveMusteriRes> CariyeBagliUreticiGetir(int cariId)
        {
            var ureticiAboneSpec = MusteriSpecifications.GetUreticiByCari(cariId);

            var musteriler = await _unitOfWork.Repository<AboneUretici>().ListAsync(ureticiAboneSpec);

            var data = musteriler.Select(x => new MusteriDTO(x)).ToList();
            return new GetAllActiveMusteriRes()
            {
                Data = data
            };
        }

        public async Task<AboneDTO> GetAboneById(int id)
        {
            var aboneSpec = MusteriSpecifications.GetAboneById(id);
            var musteriler = await _unitOfWork.Repository<Abone>().ListAsync(aboneSpec);
            var data = musteriler.Select(x => new AboneDTO(x)).FirstOrDefault();
            return data;
        }

        public async Task<GetAllActiveMusteriRes> GetBagimsizTuketiciler(int cariId)
        {
            var musteriDtos = _musterirep.GetBagimsizTuketiciler(cariId).ToList();

            return new GetAllActiveMusteriRes()
            {
                Data = musteriDtos
            };
        }
    }
}
