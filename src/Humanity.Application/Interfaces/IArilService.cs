using Humanity.Application.Models.DTOs;
using Humanity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Humanity.Application.Interfaces
{
    public interface IArilService
    {
        Task<GetOwnerConsumptionsResponse> GetOwnerConsumptions(int musteriId,DateTime startDate,DateTime endDate);


        Task<GetOwnerConsumptionsResponse> GetOwnerConsumption(int seriNo, int aboneId, DateTime startDate, DateTime endDate);


        Task<CustomerSubscriptionResponse> GetCustomerPortalSubscriptions(int musteriid);


        Task<GetEndOfMonthEndexesResponse> GetEndOfMonthEndexes(int aboneid, string donem, string donemSon,bool kaydet=false,bool uretimEndeksAl=false);

        Task<GetEndOfMonthEndexesResponse> GetCurrentEndexes(int aboneid,bool kaydet=false);

        Task<object> GetCurrentEndexesAll(int musteriId);

        Task<DateTime?> GetVeriDurumuAsync();

        Task<bool> FetchAndSaveDataAsync(DateTime today);
    }
}
