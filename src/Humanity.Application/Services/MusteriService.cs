using AutoMapper;
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
            Musteri m = mapper.Map<Musteri>(req);
            _ = await _unitOfWork.Repository<Musteri>().AddAsync(m);
            _unitOfWork.SaveChangesAsync();

            _loggerService.LogInfo("Yeni Müşteri Eklendi");

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
    }
}
