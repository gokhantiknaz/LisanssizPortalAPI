using Humanity.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Humanity.Application.Models.Responses.Dashboard
{

    public class DailyProductionConsumption:BaseEntity
    {
        /// <summary>
        /// Dönem bilgisi (YYYYMM formatında).
        /// </summary>
        public string Donem { get; set; }

        /// <summary>
        /// Gün bilgisi (1-31 arasında bir değer).
        /// </summary>
        public int Gun { get; set; }

        /// <summary>
        /// Günlük toplam üretim miktarı (kWh).
        /// </summary>
        public double ToplamUretim { get; set; }

        /// <summary>
        /// Günlük toplam tüketim miktarı (kWh).
        /// </summary>
        public double ToplamTuketim { get; set; }
    }


    public class AboneAylikTuketim : BaseEntity
    {
        public string Unvan { get; set; }
        public int SeriNo { get; set; }
        public int EndexMonth { get; set; }
        public int EndexYear { get; set; }
        public int EndexType { get; set; }
        public double T1Usage { get; set; }
        public double T2Usage { get; set; }
        public double T3Usage { get; set; }
     
    }

    public class AylikEnYuksekEnDusukTuketimGunveMiktar : BaseEntity
    {

        public string Donem { get; set; }
    
        public decimal HighConsumption { get; set; }

        public DateTime HighDay { get; set; }

        public decimal LowConsumption { get; set; }

        public DateTime LowDay { get; set; }
   
    }

    public class YillikUretimTuketim : BaseEntity
    {
        public double TotalEndex { get; internal set; }
        public int EndexType { get; internal set; }
        public string Unvan { get; set; }
    }

    public class AylikUretimTuketim : BaseEntity
    {
        public double Tuketim { get; internal set; }
        public double TuketimMahsubaDahilDegil { get; internal set; }
        public double Uretim { get; internal set; }
        public int EndexMonth { get; set; }
        public int EndexYear { get; set; }

        public double? T1Tuketim { get; internal set; }
        public double? T2Tuketim { get; internal set; }
        public double? T3Tuketim { get; internal set; }

    }


    public class AylikBazdaTumAbonelerTuketimSummary : BaseEntity
    {

        public double AralikOncekiYil { get; internal set; }
        public double Aralik { get; internal set; }
        public double Kasim { get; internal set; }
        public double Ekim { get; internal set; }
        public double Eylul { get; internal set; }
        public double Agustos { get; internal set; }
        public double Temmuz { get; internal set; }
        public double Haziran { get; internal set; }
        public double Mayis { get; internal set; }
        public double Nisan { get; internal set; }
        public double Mart { get; internal set; }
        public double Subat { get; internal set; }
        public double Ocak { get; internal set; }

        public bool MahsubaDahil { get; internal set; }
        public long SeriNo { get; internal set; }
        public string Firma { get; internal set; }
        public decimal Carpan { get; internal set; }
    }

}
