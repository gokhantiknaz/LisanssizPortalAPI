using Humanity.Application.Core.Models;
using Humanity.Application.Interfaces;
using Humanity.Application.Services;
using Humanity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Humanity.Domain.Enums.Enums;

namespace Humanity.Application.Models.DTOs.Musteri
{
    public class MusteriDTO
    {
        public int Id { get; set; }
        public string Adi { get; set; }
        public string Soyadi { get; set; }

        public string? Unvan { get; set; }

        public int CariKartId { get; set; }
        public long? Tckn { get; set; }
        public long? Vkn { get; set; }
        public Status Durum { get; set; }
        public GercekTuzel GercekTuzel { get; set; }
        public int? OzelkodId1 { get; set; }
        public int? OzelkodId2 { get; set; }
        public int? OzelkodId3 { get; set; }
        public int DagitimFirmaId { get; set; }
        public MusteriIletisimDTO MusteriIletisim { get; set; }

        public MusteriEntegrasyonDTO MusteriEntegrasyon { get; set; }

        public MusteriDTO()
        {
            
        }

        public MusteriDTO(Humanity.Domain.Entities.Musteri musteri)
        {
          
            Id =musteri.Id;
            Adi =musteri.Adi;
            Soyadi =musteri.Soyadi;
            Unvan =musteri.Unvan;
            Tckn =musteri.Tckn;
            Vkn =musteri.Vkn;
            GercekTuzel =musteri.GercekTuzel;
            OzelkodId1 =musteri.OzelkodId1;
            OzelkodId2 =musteri.OzelkodId2;
            OzelkodId3 =musteri.OzelkodId3;
            
        }
    
    }

    public class MusteriEntegrasyonDTO
    {
        public int? Id { get; set; }

        public int? MusteriId { get; set; }

        public int? ServisId { get; set; }

        public string? KullaniciAdi { get; set; }

        public string? Sifre { get; set; }

        public string? ServisAdres { get; set; }

    }

}
