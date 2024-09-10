﻿using Humanity.Application.Models.DTOs.firma;
using Humanity.Application.Models.DTOs.Musteri;
using Humanity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Humanity.Application.Models.Requests
{
    public class FirmaReq
    {
        public int Id { get; set; }
        public string FirmaAdi { get; set; }
        public string FirmaUnvan { get; set; }
        public string VergiDairesi { get; set; }
        public long? Tckn { get; set; }
        public long? Vkn { get; set; }
        public int? Durum { get; set; }
        public int GercekTuzel { get; set; }
        public int? OzelkodId1 { get; set; }
        public int? OzelkodId2 { get; set; }
        public int? OzelkodId3 { get; set; }

        public string SorumluAd { get; set; }
        public string SorumluSoyad { get; set; }

        public string SorumluTelefon { get; set; }

        public string SorumluEmail { get; set; }

        public FirmaIletisimDTO FirmaIletisim { get; set; }

        public FirmaEntegrasyonDTO FirmaEntegrasyon { get; set; }
        

    }
}
