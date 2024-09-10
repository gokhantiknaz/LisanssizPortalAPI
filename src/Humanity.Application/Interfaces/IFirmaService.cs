using Humanity.Application.Models.DTOs.Musteri;
using Humanity.Application.Models.Requests.Musteri;
using Humanity.Application.Models.Responses.Musteri;
using Humanity.Application.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Humanity.Application.Models.Requests;
using Humanity.Application.Models.DTOs.firma;

namespace Humanity.Application.Interfaces
{
    public interface IFirmaService
    {
        Task<GetFirmaRes> Create(FirmaReq req);

        Task<GetFirmaRes> Update(FirmaReq req);

        Task<FirmaIletisimDTO> GetIletisim(int cariId);
        Task<bool> Delete(int cariId);
        Task<GetFirmaRes> GetById(int id);

        Task<GetFirmaRes> GetAll();

    }
}
