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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                Adi="",
            });

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
