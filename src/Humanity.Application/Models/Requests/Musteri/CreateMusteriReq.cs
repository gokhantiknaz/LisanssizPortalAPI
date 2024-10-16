﻿using Humanity.Application.Models.DTOs.Musteri;
using Humanity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Humanity.Domain.Enums.Enums;

namespace Humanity.Application.Models.Requests.Musteri
{
    public class CreateMusteriReq
    {
        public string Adi { get; set; }
        public string Soyadi { get; set; }
        public string? Unvan { get; set; }
        public long? Tckn { get; set; }
        public long? Vkn { get; set; }
        public int? Durum { get; set; }
        //public int GercekTuzel { get; set; }
        public int? OzelkodId1 { get; set; }
        public int? OzelkodId2 { get; set; }
        public int? OzelkodId3 { get; set; }
        public MusteriIletisimDTO MusteriIletisim { get; set; }

        public int DagitimFirmaId { get; set; }

        public MusteriEntegrasyonDTO MusteriEntegrasyon { get; set; }
    }

    public class UpdateMusteriReq
    {
        public int Id { get; set; }
        public string Adi { get; set; }
        public string Soyadi { get; set; }
        public string? Unvan { get; set; }
        public long? Tckn { get; set; }
        public long? Vkn { get; set; }
        public int? Durum { get; set; }
        public int? GercekTuzel { get; set; }
        public int? OzelkodId1 { get; set; }
        public int? OzelkodId2 { get; set; }
        public int? OzelkodId3 { get; set; }
        public int DagitimFirmaId { get; set; }
        public MusteriIletisimDTO MusteriIletisim { get; set; }
        public MusteriEntegrasyonDTO MusteriEntegrasyon { get; set; }
    }
   
}
