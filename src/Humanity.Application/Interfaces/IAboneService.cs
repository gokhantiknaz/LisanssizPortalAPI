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
    public interface IAboneService
    {

        Task<GetAboneRes> GetAboneById(int AboneId);
        Task<CreateAboneRes> CreateAbone(AboneDTO req);

        Task<GetAboneRes> Update(AboneDTO req);

        //Task<GetAllActiveAboneTableRes> GetAllAbone();
        Task<AboneIletisimDTO> GetAboneIletisim(int AboneId);

        Task<AboneDTO> GetAboneUreticiAbone(int AboneId);

        Task<GetTuketiciListRes> AboneyeBagliTuketicileriGetir(int Aboneid);

        //Task<GetAllActiveAboneRes> CariyeBagliUreticiGetir(int cariId);

        Task<GetTuketiciListRes> GetBagimsizTuketiciler(int cariId);

        //Task<GetAboneRes> Update(UpdateAboneReq Abone);
    }
}
