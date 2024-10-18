﻿using AutoMapper;
using Humanity.Application.Core.Services;
using Humanity.Application.Interfaces;
using Humanity.Application.Models.DTOs.Musteri;
using Humanity.Application.Models.Requests;
using Humanity.Application.Models.Requests.Musteri;
using Humanity.Application.Models.Responses;
using Humanity.Application.Models.Responses.Musteri;
using Humanity.Application.Repositories;
using Humanity.Domain.Core.Models;
using Humanity.Domain.Core.Repositories;
using Humanity.Domain.Core.Specifications;
using Humanity.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using static Humanity.Domain.Enums.Enums;

namespace Humanity.Application.Services
{
    public class MusteriService : IMusteriService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggerService _loggerService;
        private readonly IMapper mapper;

        public MusteriService(IUnitOfWork unitOfWork, ILoggerService loggerService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _loggerService = loggerService;
            this.mapper = mapper;
        }
        public Task<GetAllActiveMusteriRes> MusteriyeBagliUreticiGetir(int musteriId)
        {
            throw new NotImplementedException();
        }

        public async Task<CreateMusteriRes> CreateMusteri(CreateMusteriReq req)
        {
            req.Durum = Status.Aktif.GetHashCode();
            Musteri m = mapper.Map<Musteri>(req);

            _ = await _unitOfWork.Repository<Musteri>().AddAsync(m);

            var miletisim = new Iletisim { Email = req.MusteriIletisim.Email ?? "", Adres = req.MusteriIletisim.Adres ?? "", CepTel = req.MusteriIletisim.CepTel ?? "", Ilid = req.MusteriIletisim.Ilid, Ilceid = req.MusteriIletisim.Ilceid };

            MusteriIletisim iletisim = new MusteriIletisim
            {
                Musteri=m,
                Iletisim = miletisim,
                IsDeleted = false,
                CreatedBy = Guid.Empty,
                CreatedOn = DateTime.UtcNow,
            };
            _ = await _unitOfWork.Repository<MusteriIletisim>().AddAsync(iletisim);

            var musterEntegrasyon = new MusteriEntegrasyon { ServisAdres = req.MusteriEntegrasyon.ServisAdres ?? "", KullaniciAdi = req.MusteriEntegrasyon.KullaniciAdi ?? "", Sifre= req.MusteriEntegrasyon.Sifre?? "", ServisId = req.MusteriEntegrasyon.ServisId };
            musterEntegrasyon.Musteri= m;

            _ = await _unitOfWork.Repository<MusteriEntegrasyon>().AddAsync(musterEntegrasyon);

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            _loggerService.LogInfo("Müşteri Kaydedildi");

            return new CreateMusteriRes() { Data = new MusteriDTO(m) };

        }

        public async Task<GetMusteriRes> GetMusteriById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<GetAllActiveMusteriRes> GetAllMusteri()
        {
            var activeSpec = new BaseSpecification<Musteri>(a => a.Durum == Domain.Enums.Enums.Status.Aktif);
            activeSpec.ApplyOrderByDescending(x => x.Id);

            var musteriler = await _unitOfWork.Repository<Musteri>().ListAsync(activeSpec);

            return new GetAllActiveMusteriRes()
            {
                Data = musteriler.Select(x => new MusteriDTO(x)).ToList()
            };
        }

        public Task<GetTuketiciListRes> GetBagimsizTuketiciler(int cariId)
        {
            throw new NotImplementedException();
        }

        public Task<GetTuketiciListRes> MusteriyeBagliTuketicileriGetir(int aboneureticiId)
        {
            throw new NotImplementedException();
        }

        private async Task<MusteriEntegrasyonDTO> GetMusteriEntegrasyon(int musteriId)
        {
            var spec = new BaseSpecification<MusteriEntegrasyon>(x => x.MusteriId == musteriId);

            var entegre = await _unitOfWork.Repository<MusteriEntegrasyon>().ListAsync(spec);

            return mapper.Map<MusteriEntegrasyonDTO>(entegre.FirstOrDefault());
        }
    }
}
