using Humanity.Application.Models.DTOs.firma;
using Humanity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Humanity.Domain.Enums.Enums;

namespace Humanity.Application.Models.DTOs.Musteri
{
    public class FirmaDTO
    {
        public int Id { get; set; }
        public string FirmaAdi { get; set; }
        public string FirmaUnvan { get; set; }
        public int Durum { get; set; }

        public string VergiDairesi { get; set; }

        public string SorumluAd { get; set; }
        public string SorumluSoyad { get; set; }
        public string SorumluTelefon { get; set; }
        public string SorumluEmail { get; set; }

        public long? Tckn { get; set; }
        public long? Vkn { get; set; }


        public GercekTuzel GercekTuzel { get; set; }
        public FirmaIletisimDTO FirmaIletisim { get; set; }

        public FirmaDTO(Humanity.Domain.Entities.Firma firma)
        {
            Id = firma.Id;
            FirmaAdi = firma.FirmaAdi;
            FirmaUnvan = firma.FirmaUnvan;
            Durum = firma.Durum.GetHashCode();
            Tckn = firma.Tckn;
            Vkn = firma.Vkn;
            VergiDairesi = firma.VergiDairesi;
            GercekTuzel = firma.GercekTuzel;
            SorumluAd = firma.SorumluAd;
            SorumluSoyad = firma.SorumluSoyad;
            SorumluTelefon = firma.SorumluTelefon;
            SorumluEmail = firma.SorumluEmail;
            FirmaIletisim = new FirmaIletisimDTO();
        }
    }
}
