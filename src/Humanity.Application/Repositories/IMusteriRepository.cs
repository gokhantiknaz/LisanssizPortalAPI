using Humanity.Application.Models.DTOs.ListDTOS;
using Humanity.Application.Models.DTOs.Musteri;
using Humanity.Domain.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Humanity.Application.Repositories
{
    public interface IMusteriRepository : IBaseRepositoryAsync<Humanity.Domain.Entities.Musteri>
    {
        MusteriDTO get(int id);
        List<TuketiciTableDTO> GetBagimsizTuketiciler(int cariId);
    }
}
