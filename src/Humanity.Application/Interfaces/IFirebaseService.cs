using Humanity.Application.Models.Responses.Dashboard;
using Humanity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Humanity.Application.Interfaces
{
    public interface IFirebaseService
    {
        Task<IEnumerable<DagitimFirma>> GetDagitimFirmalar();
        Task<IEnumerable<TarifeTanim>> GetTarifeTanim();
        Task<IEnumerable<TarifeFiyat>> GetTarifeFiyat();
        Task<TarifeFiyat> GetTarifeFiyatById(int tarife);
        Task<IEnumerable<Vergiler>> GetVergiler();
    }
}
