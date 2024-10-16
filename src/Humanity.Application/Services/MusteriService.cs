using Humanity.Application.Core.Services;
using Humanity.Application.Interfaces;
using Humanity.Application.Models.Requests;
using Humanity.Application.Models.Requests.Musteri;
using Humanity.Application.Models.Responses;
using Humanity.Application.Models.Responses.Musteri;
using Humanity.Application.Repositories;
using Humanity.Domain.Core.Models;
using Humanity.Domain.Core.Repositories;
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
using System.Text;
using System.Threading.Tasks;

namespace Humanity.Application.Services
{
    public class MusteriService : IMusteriService
    {
        public Task<GetAllActiveMusteriRes> MusteriyeBagliUreticiGetir(int musteriId)
        {
            throw new NotImplementedException();
        }

        public Task<CreateMusteriReq> CreateMusteri(CreateMusteriReq musteri)
        {
            throw new NotImplementedException();
        }

        public Task<GetMusteriRes> GetMusteriById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<GetAllActiveMusteriRes> GetAllMusteri()
        {
            throw new NotImplementedException();
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
