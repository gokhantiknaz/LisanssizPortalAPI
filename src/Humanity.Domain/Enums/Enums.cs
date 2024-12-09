using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Humanity.Domain.Enums
{
    public class Enums
    {
        public enum SahisTip
        {
            None = 0,
            Uretici = 1,
            IcTUketici,
            DisTuketici
        }

        public enum UretimSekli
        {
            Çatı = 1,
            Ges = 2
        }

        public enum LisansBilgisi
        {
            Lisanslı = 1,
            Lisanssız = 2
        }

        public enum MahsupTipi
        {
            Saatlik = 1,
            Aylık
        }

        public enum GercekTuzel
        {
            Gerçek = 1,
            Tuzel
        }

        public enum Status
        {
            OK = 0,
            Aktif = 1,
            Pasif = 2
        }

        public enum EmumSayacZaman
        {
            TekZaman = 1,
            CiftZaman = 2
        }
        public enum EnumTerim
        {

            TekTerim = 1,
            CiftTerim = 2,
            TekTerimPuant = 3,
            CiftTerimPuant = 4

        }
        public enum EnumAgOg
        {
            Ag = 1,
            Og = 2
        }

        public enum EnumEnerjiCinsi
        {
            Tum = 0,
            Gunduz = 1,
            Puant,
            Gece
        }
        public enum EnunEneriTur
        {
            Aktif = 1,
            Induktif = 2,
            Kapasitif = 3
        }
        public enum EnumEndeksDirection
        {
            Tuketim = 0,
            Uretim = 1
        }

        public enum SqlQuery
        {
            GunlukUretimTuketimMiktar,
            AyliEnDusukEnYUksekKullanimMiktarveGunleri,
            AboneAktifAylikTuketimUretim,
            YillikTuketimUretim,
            YillikUretimTuketimToplam
        }

        public enum EnumThkKod
        {
            Gunduz=1,
            Puant =2,
            Gece=3,
            ReaktifInduktif=4,
            ReaktifKapasitif=5,
            Kdv=6,
            GucBedeli=7,
            GucAsimBedeli=8,
            DagitimBaglantiBedeli = 9
        }
    }
}