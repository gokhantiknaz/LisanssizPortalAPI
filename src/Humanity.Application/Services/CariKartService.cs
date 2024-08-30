using Humanity.Application.Core.Services;
using Humanity.Application.Interfaces;
using Humanity.Application.Models.DTOs.Musteri;
using Humanity.Application.Models.Requests.Musteri;
using Humanity.Application.Models.Responses.Musteri;
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
    public class CariKartService : ICariKartService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggerService _loggerService;

        public CariKartService(IUnitOfWork unitOfWork, ILoggerService loggerService)
        {
            _unitOfWork = unitOfWork;
            _loggerService = loggerService;

        }
        public async Task<CreateCariKartRes> Create(CreateCariKartReq req)
        {
            var cari = await _unitOfWork.Repository<CariKart>().AddAsync(new CariKart
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
            });

            _ = _unitOfWork.Repository<CariKart>().AddAsync(cari);

            var cariIletisim = new Iletisim { Email = req.CariIletisim.Email ?? "", Adres = req.CariIletisim.Adres ?? "", CepTel = req.CariIletisim.CepTel ?? "", Ilid = req.CariIletisim.Ilid, Ilceid = req.CariIletisim.Ilceid };

            CariIletisim iletisim = new CariIletisim
            {
                CariKart = cari,
                Iletisim = cariIletisim,
                IsDeleted = false,
                CreatedBy = Guid.Empty,
                CreatedOn = DateTime.UtcNow,
            };
            _ = await _unitOfWork.Repository<CariIletisim>().AddAsync(iletisim);


            await _unitOfWork.SaveChangesAsync();

            _loggerService.LogInfo("Yeni Cari Kart Eklendi");

            return new CreateCariKartRes() { Data = new CariKartDTO(cari, this) };
        }

        public async Task<CreateCariKartRes> Update(UpdateCariKartReq req)
        {
            var cari = await _unitOfWork.Repository<CariKart>().GetByIdAsync(req.Id);

            cari.Adi = req.Adi;
            cari.Soyadi = req.Soyadi;
            cari.CreatedOn = DateTime.UtcNow;
            cari.Durum = Status.Aktif;
            cari.GercekTuzel = (Domain.Enums.Enums.GercekTuzel)req.GercekTuzel;
            cari.IsDeleted = false;
            cari.Tckn = req.Tckn;
            cari.Vkn = req.Vkn;
            cari.Unvan = req.Unvan;
            cari.OzelkodId1 = req.OzelkodId1;
            cari.OzelkodId2 = req.OzelkodId2;
            cari.OzelkodId3 = req.OzelkodId3;


            _unitOfWork.Repository<CariKart>().Update(cari);

            Iletisim cariiletisim;
            if (req.CariIletisim.IletisimId > 0)
            {
                cariiletisim = await _unitOfWork.Repository<Iletisim>().GetByIdAsync(req.CariIletisim.IletisimId);

                cariiletisim.Adres = req.CariIletisim.Adres ?? "";
                cariiletisim.Ilid = req.CariIletisim.Ilid.HasValue ? req.CariIletisim.Ilid.Value : null;
                cariiletisim.Ilceid = req.CariIletisim.Ilceid.HasValue ? req.CariIletisim.Ilceid.Value : null;
                cariiletisim.CepTel = req.CariIletisim.CepTel ?? "";
                cariiletisim.Email = req.CariIletisim.Email ?? "";
                _unitOfWork.Repository<Iletisim>().Update(cariiletisim);
            }
            else
            {
                var cariIletisim = new Iletisim { Email = req.CariIletisim.Email ?? "", Adres = req.CariIletisim.Adres ?? "", CepTel = req.CariIletisim.CepTel ?? "", Ilid = req.CariIletisim.Ilid, Ilceid = req.CariIletisim.Ilceid };

                CariIletisim iletisimNEw = new CariIletisim
                {
                    CariKart = cari,
                    Iletisim = cariIletisim,
                    IsDeleted = false,
                    CreatedBy = Guid.Empty,
                    CreatedOn = DateTime.UtcNow,
                };

                _ = await _unitOfWork.Repository<CariIletisim>().AddAsync(iletisimNEw);
            }


            await _unitOfWork.SaveChangesAsync();

            _loggerService.LogInfo("Cari Kart Güncellendi.");

            return new CreateCariKartRes() { Data = new CariKartDTO(cari, this) };
        }

        public async Task<bool> Delete(int cariId)
        {
            var cari = await _unitOfWork.Repository<CariKart>().GetByIdAsync(cariId);
            cari.IsDeleted = true;
            _unitOfWork.Repository<CariKart>().Update(cari);

            return await _unitOfWork.SaveChangesAsync() > 0;

        }

        public async Task<MusteriIletisimDTO> GetIletisim(int cariId)
        {
            var activeSpec = new BaseSpecification<CariIletisim>(a => a.CariKartId == cariId);
            activeSpec.AddInclude(a => a.Iletisim);
            var musterilerIletisimler = await _unitOfWork.Repository<CariIletisim>().ListAsync(activeSpec);

            if (musterilerIletisimler.Count == 0)
                return new MusteriIletisimDTO() { };
            return new MusteriIletisimDTO()
            {
                IletisimId = musterilerIletisimler[0].IletisimId,
                Adres = musterilerIletisimler[0].Iletisim.Adres,
                CepTel = musterilerIletisimler[0].Iletisim.CepTel,
                Email = musterilerIletisimler[0].Iletisim.Email,
                Ilceid = musterilerIletisimler[0].Iletisim.Ilceid,
                Ilid = musterilerIletisimler[0].Iletisim.Ilid
            };
        }



        public async Task<GetAllActiveCariKartRes> GetAllCariKart()
        {
            var cariList = await _unitOfWork.Repository<CariKart>().ListAsync(new BaseSpecification<CariKart>(x => x.IsDeleted == false));

            return new GetAllActiveCariKartRes()
            {
                Data = cariList.Select(x => new CariKartDTO(x, this)).ToList()
            };
        }
    }
}
