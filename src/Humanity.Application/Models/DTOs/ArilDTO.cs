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

        public int DagitimFİrmaId { get; set; }

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


    public class Consumption
    {
        public decimal cn { get; set; }
        public decimal ri { get; set; }
        public decimal rc { get; set; }
        public decimal gn { get; set; }
        public decimal rio { get; set; }
        public decimal rco { get; set; }
        public decimal ml { get; set; }
        public decimal rir { get; set; }
        public decimal rcr { get; set; }
        public int st { get; set; }
        public decimal addcn { get; set; }
        public decimal addgn { get; set; }
        public long pd { get; set; }
    }

    public class Sum
    {
        public decimal Con { get; set; }
        public decimal Gen { get; set; }
        public decimal RI { get; set; }
        public decimal RC { get; set; }
        public decimal RIOUT { get; set; }
        public decimal RCOUT { get; set; }
    }

    public class Min
    {
        public decimal Con { get; set; }
        public long ConDate { get; set; }
        public decimal Gen { get; set; }
        public long GenDate { get; set; }
        public decimal RI { get; set; }
        public long RIDate { get; set; }
        public decimal RC { get; set; }
        public long RCDate { get; set; }
        public decimal RIOUT { get; set; }
        public long RIOUTDate { get; set; }
        public decimal RCOUT { get; set; }
        public long RCOUTDate { get; set; }
    }

    public class Max
    {
        public decimal Con { get; set; }
        public long ConDate { get; set; }
        public decimal Gen { get; set; }
        public long GenDate { get; set; }
        public decimal RI { get; set; }
        public long RIDate { get; set; }
        public decimal RC { get; set; }
        public long RCDate { get; set; }
        public decimal RIOUT { get; set; }
        public long RIOUTDate { get; set; }
        public decimal RCOUT { get; set; }
        public long RCOUTDate { get; set; }
    }

    public class Avg
    {
        public decimal Con { get; set; }
        public decimal Gen { get; set; }
        public decimal RI { get; set; }
        public decimal RC { get; set; }
        public decimal RIOUT { get; set; }
        public decimal RCOUT { get; set; }
    }

    public class Summary
    {
        public long FirstProfileDate { get; set; }
        public long LastProfileDate { get; set; }
        public Sum Sum { get; set; }
        public Min Min { get; set; }
        public Max Max { get; set; }
        public Avg Avg { get; set; }
    }

    public class ConsumptionDetail
    {
        public long pd { get; set; }        // Tarih
        public decimal cn { get; set; }      // Tüketim miktarı
        public int ml { get; set; }          // Çarpan
        public int st { get; set; }          // Durum
        public decimal ri { get; set; }      // Reaktif tüketim
        public decimal rc { get; set; }      // Reaktif üretim
    }

    public class ArilSaatlikResponse
    {
        public int OwnerSerno { get; set; }
        public int OwnerType { get; set; }
        public List<ConsumptionDetail> InConsumption { get; set; }
        public List<ConsumptionDetail> OutConsumption { get; set; }
        public List<object> LoadProfiles { get; set; }
        public decimal TotalMultiplier { get; set; }
        public string OwnerIdentifier { get; set; }
        public string OwnerIdentifierSec { get; set; }
        public List<Consumption> MergedConsumptions { get; set; }
        public Summary Summary { get; set; }  // Yeni eklenen Summary özelliği
    }

}
