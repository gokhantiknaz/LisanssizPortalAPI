using AutoMapper;
using Humanity.Application.Core.Services;
using Humanity.Application.Interfaces;
using Humanity.Application.Models.DTOs.firma;
using Humanity.Application.Models.DTOs.Musteri;
using Humanity.Application.Models.Requests;
using Humanity.Application.Models.Responses;
using Humanity.Application.Models.Responses.Musteri;
using Humanity.Domain.Core.Repositories;
using Humanity.Domain.Core.Specifications;
using Humanity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using static Humanity.Domain.Enums.Enums;

namespace Humanity.Application.Services
{
    public class FirmaService : IFirmaService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggerService _loggerService;
        private readonly IMapper mapper;

        public FirmaService(IUnitOfWork unitOfWork, ILoggerService loggerService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _loggerService = loggerService;
            this.mapper = mapper;
        }
        public async Task<GetFirmaRes> Create(FirmaReq req)
        {
            var firma = await _unitOfWork.Repository<Firma>().AddAsync(new Firma
            {
                FirmaAdi = req.FirmaAdi,
                FirmaUnvan = req.FirmaUnvan,
                CreatedOn = DateTime.UtcNow,
                Durum = Status.Aktif,
                GercekTuzel = (Domain.Enums.Enums.GercekTuzel)req.GercekTuzel,
                IsDeleted = false,
                Tckn = req.Tckn,
                Vkn = req.Vkn,
                OzelkodId1 = req.OzelkodId1,
                OzelkodId2 = req.OzelkodId2,
                OzelkodId3 = req.OzelkodId3,
                SorumluAd = req.SorumluAd,
                SorumluSoyad = req.SorumluSoyad,
                SorumluEmail = req.SorumluEmail,
                SorumluTelefon = req.SorumluTelefon,
                VergiDairesi = req.VergiDairesi
            });

            _ = _unitOfWork.Repository<Firma>().AddAsync(firma);

            var firmaIletisim = new Iletisim { Email = req.FirmaIletisim.Email ?? "", Adres = req.FirmaIletisim.Adres ?? "", CepTel = req.FirmaIletisim.CepTel ?? "", Ilid = req.FirmaIletisim.Ilid, Ilceid = req.FirmaIletisim.Ilceid };

            FirmaIletisim iletisim = new FirmaIletisim
            {
                Firma = firma,
                Iletisim = firmaIletisim,
                IsDeleted = false,
                CreatedBy = Guid.Empty,
                CreatedOn = DateTime.UtcNow,
            };
            _ = await _unitOfWork.Repository<FirmaIletisim>().AddAsync(iletisim);



            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            _loggerService.LogInfo("Firma Kaydedildi");

            return new GetFirmaRes() { Data = new FirmaDTO(firma) };
        }

        public async Task<GetFirmaRes> Update(FirmaReq req)
        {
            var firma = await _unitOfWork.Repository<Firma>().GetByIdAsync(req.Id);

            firma.FirmaAdi = req.FirmaAdi;
            firma.FirmaUnvan = req.FirmaUnvan;
            firma.CreatedOn = DateTime.UtcNow;
            firma.Durum = Status.Aktif;
            firma.GercekTuzel = (Domain.Enums.Enums.GercekTuzel)req.GercekTuzel;
            firma.IsDeleted = false;
            firma.Tckn = req.Tckn;
            firma.Vkn = req.Vkn;
            firma.OzelkodId1 = req.OzelkodId1;
            firma.OzelkodId2 = req.OzelkodId2;
            firma.OzelkodId3 = req.OzelkodId3;


            _unitOfWork.Repository<Firma>().Update(firma);

            Iletisim cariiletisim;
            if (req.FirmaIletisim.IletisimId > 0)
            {
                cariiletisim = await _unitOfWork.Repository<Iletisim>().GetByIdAsync(req.FirmaIletisim.IletisimId);

                cariiletisim.Adres = req.FirmaIletisim.Adres ?? "";
                cariiletisim.Ilid = req.FirmaIletisim.Ilid.HasValue ? req.FirmaIletisim.Ilid.Value : null;
                cariiletisim.Ilceid = req.FirmaIletisim.Ilceid.HasValue ? req.FirmaIletisim.Ilceid.Value : null;
                cariiletisim.CepTel = req.FirmaIletisim.CepTel ?? "";
                cariiletisim.Email = req.FirmaIletisim.Email ?? "";
                _unitOfWork.Repository<Iletisim>().Update(cariiletisim);
            }
            else
            {
                var cariIletisim = new Iletisim { Email = req.FirmaIletisim.Email ?? "", Adres = req.FirmaIletisim.Adres ?? "", CepTel = req.FirmaIletisim.CepTel ?? "", Ilid = req.FirmaIletisim.Ilid, Ilceid = req.FirmaIletisim.Ilceid };

                FirmaIletisim iletisimNEw = new FirmaIletisim
                {
                    Firma = firma,
                    Iletisim = cariIletisim,
                    IsDeleted = false,
                    CreatedBy = Guid.Empty,
                    CreatedOn = DateTime.UtcNow,
                };

                _ = await _unitOfWork.Repository<FirmaIletisim>().AddAsync(iletisimNEw);
            }


            await _unitOfWork.SaveChangesAsync();

            _loggerService.LogInfo("Firma Kartı Güncellendi.");

            return new GetFirmaRes() { Data = new FirmaDTO(firma) };
        }

        public async Task<bool> Delete(int cariId)
        {
            throw new NotImplementedException();
        }

        public async Task<GetFirmaRes> GetById(int id)
        {
            var firma = await _unitOfWork.Repository<Firma>().GetByIdAsync(id);

            if (firma == null)
                throw new Exception("Cari Kart Bulunamadı");
            //throw NotFoundException("Cari");

            //iletisim bilgisi
            var firmaIletisimDto = await GetFirmaIletisim(id);

            var data = new FirmaDTO(firma) { FirmaIletisim = new FirmaIletisimDTO(firmaIletisimDto) };

            
            return new GetFirmaRes()
            {
                Data = data
            };
        }

        public async Task<GetFirmaRes> GetAll()
        {
            var firmaList = await _unitOfWork.Repository<Firma>().ListAllAsync();

            if (firmaList == null || firmaList.Count == 0)
                throw new Exception("Firma bulunamadı");
            //throw NotFoundException("Cari");

            //iletisim bilgisi
            var firma = firmaList.FirstOrDefault();

            var firmaIletisimDto = await GetFirmaIletisim(firma.Id);


            var data = new FirmaDTO(firma) { FirmaIletisim = new FirmaIletisimDTO(firmaIletisimDto) };

            return new GetFirmaRes()
            {
                Data = data
            };
        }


        public async Task<FirmaIletisimDTO> GetIletisim(int firmaId)
        {
            throw new NotImplementedException();
        }

        private async Task<FirmaIletisim> GetFirmaIletisim(int firmaId)
        {
            var spec = new BaseSpecification<FirmaIletisim>(x => x.FirmaId == firmaId);
            spec.AddInclude(a => a.Iletisim);

            var iletisim = await _unitOfWork.Repository<FirmaIletisim>().ListAsync(spec);

            return iletisim.FirstOrDefault();
        }



    }
}
