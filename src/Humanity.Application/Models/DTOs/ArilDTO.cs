using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Humanity.Application.Models.DTOs
{
    public class GetOwnerConsumptionsResponse
    {

    }

    public class CustomerSubscriptionResponse
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int TotalItemCount { get; set; }
        public int TotalPageCount { get; set; }
        public List<CustomerSubscription> ResultList { get; set; }
    }

    public class CustomerSubscription
    {
        public int SubscriptionSerno { get; set; }
        public string IdentifierValue { get; set; }
        public string IdentifierValueSec { get; set; }
        public int DefinitionType { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        public string MeterSerial { get; set; }
        public string MeterBrand { get; set; }
        public string MeterModel { get; set; }
        public decimal Multiplier { get; set; }
        public long LastEndexDate { get; set; }
        public long LastProfileDate { get; set; }
        public decimal MinCapasitiveRate { get; set; }
        public decimal MinInductiveRate { get; set; }
        public decimal InstalledPower { get; set; }
        public decimal AccordPower { get; set; }
        public string GroupInfo { get; set; }
        public long MeterPointOwnerAssignDate { get; set; }
        public long SubscriberMultiplierChangeDate { get; set; }
        public object CustomerFields { get; set; }

        public string ScheduleCode { get; set; }
        
        public string DefinitionTypeStr
        {
            get
            {
                return DefinitionType switch
                {
                    2 => "Tüketim Noktası",
                    15 => "Üretim Noktası",
                    _ => "Bilinmeyen Nokta" // varsayılan değer
                };
            }
        }
    }



    public class GetEndOfMonthEndexesResponse
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int TotalItemCount { get; set; }
        public int TotalPageCount { get; set; }
        public List<EndexData> ResultList { get; set; }
    }


    public class EndexData
    {
        public int EndexYear { get; set; }
        public int EndexMonth { get; set; }
        public long EndexDate { get; set; }
        public int EndexType { get; set; }
        public double T1Endex { get; set; }
        public double T2Endex { get; set; }
        public double T3Endex { get; set; }
        public double T4Endex { get; set; }
        public double ReactiveCapasitive { get; set; }
        public double MaxDemand { get; set; }
        public long DemandDate { get; set; }
        public double TSum { get; set; }
        public double ReactiveInductive { get; set; }
        public string UpdateMongoID { get; set; }
        public int SensorSerno { get; set; }
        public string SensorIdentifier { get; set; }
    }

   


}
