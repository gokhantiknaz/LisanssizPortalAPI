﻿using Humanity.Application.Models.DTOs.Musteri;
using Humanity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Humanity.Application.Models.DTOs.ListDTOS
{
  
    public class TuketiciTableDTO
    {
        public int Id { get; set; } //abonetuketiciid

        public int AboneId { get; set; }

        public int UreticiAboneId { get; set; }
        public string Adi { get; set; }
        public string Soyadi { get; set; }

        public string? Unvan { get; set; }

        public int? Ilid { get; set; }

        public int? Ilceid { get; set; }
        public DateTime BaslamaZamani { get; set; }

        public AboneDTO UreticiAbone { get; set; }

        public bool IsDeleted { get; set; }

        public string SerNo { get; set; }

        public int DefinitionType { get; set; }


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

        public TuketiciTableDTO()
        {
            
        }

        public TuketiciTableDTO(Humanity.Domain.Entities.Musteri musteri)
        {
            Adi = musteri.Adi;
            Soyadi = musteri.Soyadi;
            Unvan = musteri.Unvan;
            //BaslamaZamani = tuketici.BaslamaZamani;
            Ilid = musteri.MusteriIletisim?.Iletisim?.Ilid;
            Ilceid = musteri.MusteriIletisim?.Iletisim?.Ilceid;
            IsDeleted = false;
        }

        public TuketiciTableDTO(AboneTuketici tuketici)
        {
            AboneId = tuketici.AboneId;
            Adi = tuketici.Abone.Adi;
            Soyadi = tuketici.Abone.Soyadi;
            Unvan = tuketici.Abone.Unvan;
            BaslamaZamani = tuketici.BaslamaZamani;
            Ilid = tuketici.Abone.AboneIletisim.Iletisim.Ilid;
            Ilceid = tuketici.Abone.AboneIletisim.Iletisim.Ilceid;
            IsDeleted = tuketici.IsDeleted;
        }
    }
}
