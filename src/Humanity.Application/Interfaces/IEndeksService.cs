using Humanity.Application.Models.DTOs;
using Humanity.Application.Models.DTOs.firma;
using Humanity.Application.Models.Requests;
using Humanity.Application.Models.Responses;
using Humanity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Humanity.Application.Interfaces
{
    public interface IEndeksService
    {
        Task<List<EndeksResponses>> GetAboneSaatlikEndeks(int aboneId, string donem);

        Task<EndeksResponses> Create(List<SaatlikEndeksRequest> req);

        Task<List<AylikEndeksRes>> GetAboneDonemEndeks(int aboneId, string donem);

        Task<List<EndeksResponses>> GetAboneSaatlikEndeksOzet(int aboneId, string donem);
        
        Task<bool> AylikEndeksKaydet(int aboneid, GetEndOfMonthEndexesResponse? res);
    }
}
