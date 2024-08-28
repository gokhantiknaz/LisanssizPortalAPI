using Humanity.Application.Core.Services;
using Humanity.Application.Interfaces;
using Humanity.Application.Models.DTOs;
using Humanity.Application.Models.DTOs.Musteri;
using Humanity.Application.Models.Requests.Musteri;
using Humanity.Application.Models.Responses;
using Humanity.Application.Models.Responses.Musteri;
using Humanity.Domain.Core.Repositories;
using Humanity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Humanity.Domain.Enums.Enums;

namespace Humanity.Application.Services
{
    public class MusteriService : IMusteriService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggerService _loggerService;

        public MusteriService(IUnitOfWork unitOfWork, ILoggerService loggerService)
        {
            _unitOfWork = unitOfWork;
            _loggerService = loggerService;
        }
        public async Task<CreateMusteriRes> CreateMusteri(CreateMusteriReq req)
        {
            var musteri = await _unitOfWork.Repository<Musteri>().AddAsync(new Musteri
            {
                Adi = req.Adi,
                Soyadi = req.Soyadi,
                CreatedOn = DateTime.UtcNow,
                Durum = 1,
                GercekTuzel = (Domain.Enums.Enums.GercekTuzel)req.GercekTuzel,
                IsDeleted = false,
                Tckn = req.Tckn,
                Vkn = req.Vkn,
                Unvan = req.Unvan,
                OzelkodId1 = req.OzelkodId1,
                OzelkodId2 = req.OzelkodId2,
                OzelkodId3 = req.OzelkodId3,
            });

            var newiletisimMusteri = new Iletisim { Email = req.MusteriIletisim.Email ?? "", Adres = req.MusteriIletisim.Adres ?? "", CepTel = req.MusteriIletisim.CepTel ?? "", Ilid = req.MusteriIletisim.Ilid, Ilceid = req.MusteriIletisim.Ilceid };

            MusteriIletisim iletisimMusteri = new MusteriIletisim
            {
                Musteri = musteri,
                Iletisim = newiletisimMusteri,
                IsDeleted = false,
                CreatedBy = Guid.Empty,
                CreatedOn = DateTime.UtcNow,
            };
            _ = await _unitOfWork.Repository<MusteriIletisim>().AddAsync(iletisimMusteri);

            var abone = await _unitOfWork.Repository<Abone>().AddAsync(new Abone
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
                Musteri = musteri
            });

            if (SahisTip.Uretici == abone.SahisTip)
            {
                var lisansBilgi = new UreticiLisans()
                {
                    Abone = abone,
                    CagrimektupTarihi = req.Abone.LisansBilgileri.CagrimektupTarihi,
                    LisansBilgisi = req.Abone.LisansBilgileri.LisansBilgisi,
                    MahsupTipi = req.Abone.LisansBilgileri.MahsupTipi,
                    UretimBaslama = req.Abone.LisansBilgileri.UretimBaslama,
                    UretimSekli = req.Abone.LisansBilgileri.UretimSekli
                };

                _ = await _unitOfWork.Repository<UreticiLisans>().AddAsync(lisansBilgi);

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

            var newiletisimAbone = new Iletisim { CepTel = "", Email = "", Adres = "", Ilid = req.Abone.AboneIletisim.Ilid, Ilceid = req.Abone.AboneIletisim.Ilceid };

            AboneIletisim iletisimabone = new AboneIletisim
            {
                Abone = abone,
                Iletisim = newiletisimAbone,
                CreatedBy = Guid.Empty,
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false
            };

            _ = _unitOfWork.Repository<AboneIletisim>().AddAsync(iletisimabone);



            await _unitOfWork.SaveChangesAsync();

            _loggerService.LogInfo("Yeni Müşteri Eklendi");

            return new CreateMusteriRes() { Data = new MusteriDTO(musteri) };
        }

        public async Task<GetAllActiveMusteriRes> GetAllMusteri()
        {
            throw new NotImplementedException();
        }

        public async Task<ValidateMusteriRes> ValidateMusteri(ValidateMusteriReq req)
        {
            throw new NotImplementedException();
        }
    }
}
