using Humanity.Application.Models.DTOs.Musteri;
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
            Adi = tuketici.Abone.Musteri.Adi;
            Soyadi = tuketici.Abone.Musteri.Soyadi;
            Unvan = tuketici.Abone.Musteri.Unvan;
            BaslamaZamani = tuketici.BaslamaZamani;
            Ilid = tuketici.Abone.Musteri.MusteriIletisim.Iletisim.Ilid;
            Ilceid = tuketici.Abone.Musteri.MusteriIletisim.Iletisim.Ilceid;
            IsDeleted = tuketici.IsDeleted;
        }
    }
}
