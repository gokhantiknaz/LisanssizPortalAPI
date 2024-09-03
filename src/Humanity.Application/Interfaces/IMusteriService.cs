using Humanity.Application.Models.DTOs.Musteri;
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

        Task<GetMusteriRes> GetMusteriById(int id);

        Task<ValidateMusteriRes> ValidateMusteri(ValidateMusteriReq req);

        Task<GetAllActiveMusteriTableRes> GetAllMusteri();
        Task<MusteriIletisimDTO> GetMusteriIletisim(int musteriId);

        Task<AboneDTO> GetMusteriUreticiAbone(int musteriId);

        Task<GetTuketiciListRes> MusteriyeBagliTuketicileriGetir(int musteriid);

        Task<GetAllActiveMusteriRes> CariyeBagliUreticiGetir(int cariId);
        
        Task<AboneDTO> GetAboneById(int id);
        Task<GetTuketiciListRes> GetBagimsizTuketiciler(int cariId);
        Task<GetMusteriRes> Update(UpdateMusteriReq musteri);
    }
}
