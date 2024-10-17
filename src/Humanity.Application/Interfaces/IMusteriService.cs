﻿using Humanity.Application.Models.Requests.Musteri;
using Humanity.Application.Models.Responses.Musteri;
using Humanity.Application.Models.Responses;
using Humanity.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Humanity.Application.Interfaces
{
    public interface IMusteriService
    {
        Task<GetMusteriRes> GetMusteriById(int id);
        Task<CreateMusteriRes> CreateMusteri(CreateMusteriReq musteri);
        Task<GetAllActiveMusteriRes> GetAllMusteri();
        Task<GetAllActiveMusteriRes> MusteriyeBagliUreticiGetir(int musteriId);
        Task<GetTuketiciListRes> MusteriyeBagliTuketicileriGetir(int aboneureticiId);
        Task<GetTuketiciListRes> GetBagimsizTuketiciler(int cariId);
    }
}
