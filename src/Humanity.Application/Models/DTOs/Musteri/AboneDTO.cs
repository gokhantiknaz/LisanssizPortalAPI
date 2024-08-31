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
        public int Tarife { get; set; }
        public string EtsoKodu { get; set; }
        public int DagitimFirmaId { get; set; }
        public long SeriNo { get; set; }
        public double SozlesmeGucu { get; set; }
        public double BaglantiGucu { get; set; }
        public double KuruluGuc { get; set; }
        public SahisTip SahisTip { get; set; }
        public int Terim { get; set; }
        public int Agog { get; set; }
        public UreticiDTO? LisansBilgileri { get; set; }
        public AboneSayac? AboneSayac { get; set; }
        public AboneIletisimDTO AboneIletisim { get; set; }

        public AboneDTO()
        {

        }

        public AboneDTO(Abone abone)
        {
            Tarife = abone.Tarife;
            EtsoKodu = abone.EtsoKodu;
            DagitimFirmaId = abone.DagitimFirmaId;
            SeriNo = abone.SeriNo;
            SozlesmeGucu = abone.SozlesmeGucu;
            KuruluGuc = abone.KuruluGuc;
            BaglantiGucu = abone.BaglantiGucu;
            SahisTip = abone.SahisTip; Terim = abone.Terim; Agog = abone.Agog;
        }
    }


    public class UreticiDTO
    {
        public int Id { get; set; }

        public int AboneId { get; set; }

        public UretimSekli UretimSekli { get; set; }

        public string UretimSekliText { get; set; }
        public LisansBilgisi LisansBilgisi { get; set; }
        public string LisansBilgisiText { get; set; }

        public DateTime UretimBaslama { get; set; }
        public DateTime CagrimektupTarihi { get; set; }
        public int MahsupTipi { get; set; }
        public string MahsupTipiText { get; set; }

        public UreticiDTO(AboneUretici a)
        {
            Id = a.Abone.Musteri.Id;
            AboneId = a.AboneId;
            UretimSekli = a.UretimSekli;
            UretimSekliText = a.UretimSekli.ToString();
            LisansBilgisi = a.LisansBilgisi;
            LisansBilgisiText = a.LisansBilgisi.ToString();
            UretimBaslama = a.UretimBaslama;
            CagrimektupTarihi = a.CagrimektupTarihi;
            MahsupTipi = a.MahsupTipi;
            MahsupTipiText = a.MahsupTipi == 1 ? "Saatlik" : "Aylık";

        }

    }
}
