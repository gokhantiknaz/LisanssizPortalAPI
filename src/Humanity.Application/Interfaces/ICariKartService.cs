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
    public interface ICariKartService
    {
        Task<CreateCariKartRes> Create(CreateCariKartReq req);

        Task<CreateCariKartRes> Update(UpdateCariKartReq req);

        Task<GetAllActiveCariKartRes> GetAllCariKart();

        Task<MusteriIletisimDTO> GetIletisim(int cariId);
    }
}
