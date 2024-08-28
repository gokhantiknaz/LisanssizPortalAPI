using Humanity.Application.Models.Requests;
using Humanity.Application.Models.Requests.Musteri;
using Humanity.Application.Models.Responses;
using Humanity.Application.Models.Responses.Musteri;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Humanity.Application.Interfaces
{
    public interface IMusteriService
    {
        Task<CreateMusteriRes> CreateMusteri(CreateMusteriReq req);

        Task<ValidateMusteriRes> ValidateMusteri(ValidateMusteriReq req);

        Task<GetAllActiveMusteriRes> GetAllMusteri();
    }
}
