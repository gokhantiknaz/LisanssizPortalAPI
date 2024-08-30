using Humanity.Application.Core.Models;
using Humanity.Application.Interfaces;
using Humanity.Application.Services;
using Humanity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Humanity.Domain.Enums.Enums;

namespace Humanity.Application.Models.DTOs.Musteri
{
    public class MusteriDTO
    {
        private readonly IMusteriService _musteriService;


        public int Id { get; set; }
        public string Adi { get; set; }
        public string Soyadi { get; set; }
        public int Durum { get; set; }

        public decimal KuruluGuc { get; set; }

        public decimal SozlesmeGucu { get; set; }

        public AboneDTO Uretici { get; set; }

        public AboneDTO GetUretici(int musteriId)
        {
            return new AboneDTO();
        }

        public MusteriIletisimDTO MusteriIletisim { get; set; }

        public MusteriIletisimDTO GetMusteriIletisim(int musteriId)
        {
            var musteriIletisim = _musteriService.GetMusteriIletisim(musteriId);
            return musteriIletisim.Result;
        }

        public MusteriDTO(IMusteriService musteriService, Humanity.Domain.Entities.Musteri musteri)
        {
            _musteriService = musteriService;
            Id = musteri.Id;
            Adi = musteri.Adi;
            Soyadi = musteri.Soyadi;
            MusteriIletisim = GetMusteriIletisim(musteri.Id);
            Uretici = GetUretici(musteri.Id);
        }
    }

    public class CariKartDTO
    {
        private readonly ICariKartService _cariKartService;

        public int Id { get; set; }
        public string Adi { get; set; }
        public string Soyadi { get; set; }
        public int Durum { get; set; }

        public string Unvan { get; set; }

        public long? Tckn { get; set; }
        public long? Vkn { get; set; }
        public long RefNo{ get; set; }

        public GercekTuzel GercekTuzel { get; set; }
        public MusteriIletisimDTO CariIletisim { get; set; }


        public MusteriIletisimDTO GetIletisim(int cariKartId)
        {
            var musteriIletisim = _cariKartService.GetIletisim(cariKartId);
            return musteriIletisim.Result;
        }

        public CariKartDTO(Humanity.Domain.Entities.CariKart cari, ICariKartService cariKartService)
        {
            _cariKartService = cariKartService;
            Id = cari.Id;
            Adi = cari.Adi;
            Soyadi = cari.Soyadi;
            Unvan = cari.Unvan;
            Durum = cari.Durum.GetHashCode();
            Tckn = cari.Tckn;
            Vkn = cari.Vkn;
            GercekTuzel = cari.GercekTuzel;

            CariIletisim = GetIletisim(cari.Id);
    }
}
}
