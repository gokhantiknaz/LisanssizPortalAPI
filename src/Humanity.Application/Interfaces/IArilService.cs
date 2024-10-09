using Humanity.Application.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Humanity.Application.Interfaces
{
    public interface IArilService
    {
        Task<GetOwnerConsumptionsResponse> GetOwnerConsumptions(DateTime startDate,DateTime endDate);
    }
}
