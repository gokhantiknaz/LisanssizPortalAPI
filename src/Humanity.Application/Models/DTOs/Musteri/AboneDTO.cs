using Humanity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Humanity.Domain.Enums.Enums;

namespace Humanity.Application.Models.DTOs.Musteri
{
    public class AboneDTO
    {
        public int Id { get; set; }

        public int MusteriId { get; set; }
        public int Tarife { get; set; }
        public string? EtsoKodu { get; set; }

        public int DagitimFirmaId { get; set; }
        public long? SeriNo { get; set; }
        public double? SozlesmeGucu { get; set; }
        public double? BaglantiGucu { get; set; }
        public double? KuruluGuc { get; set; }
        public SahisTip SahisTip { get; set; }
        public int Terim { get; set; }
        public int Agog { get; set; }
        public UreticiDTO? UreticiBilgileri { get; set; }
        public AboneSayac? AboneSayac { get; set; }

        public int? OzelkodId1 { get; set; }
        public int? OzelkodId2 { get; set; }
        public int? OzelkodId3 { get; set; }

        public string? Adi { get; set; }
        public string? Soyadi { get; set; }
        public string? Unvan { get; set; }

        public long? Tckn { get; set; }
        public long? Vkn { get; set; }

        public bool MahsubaDahil { get; set; }

        public List<TuketiciListDTO>? TuketiciList { get; set; }

        public List<SayacListDTO>? SayacList { get; set; }

        public AboneIletisimDTO AboneIletisim { get; set; }


        public AboneDTO()
        {

        }

        public AboneDTO(Abone abone)
        {
            Id = abone.Id;
            SahisTip = abone.SahisTip; Terim = abone.Terim; Agog = abone.Agog;
        }
    }

    public class UreticiDTO
    {
        public int Id { get; set; }

        public int AboneId { get; set; }

        public UretimSekli UretimSekli { get; set; }

        public string UretimSekliText { get { return UretimSekli.ToString(); } }
        public LisansBilgisi LisansBilgisi { get; set; }
        public string LisansBilgisiText { get { return LisansBilgisi.ToString(); } }

        public DateTime UretimBaslama { get; set; }
        public DateTime CagrimektupTarihi { get; set; }
        public int MahsupTipi { get; set; }
        public string MahsupTipiText { get { return MahsupTipi == 1 ? "Saatlik" : "Aylık"; } }

        public UreticiDTO()
        {

        }

        public UreticiDTO(AboneUretici a)
        {
            Id = a.Id;
            AboneId = a.AboneId;
            UretimSekli = a.UretimSekli;
            LisansBilgisi = a.LisansBilgisi;
            UretimBaslama = a.UretimBaslama;
            CagrimektupTarihi = a.CagrimektupTarihi;
            MahsupTipi = a.MahsupTipi;
        }
    }
}
