using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Humanity.Domain.Enums.Enums;

namespace Humanity.Domain.Entities
{

    public class TarifeTanim
    {
        public int Id { get; set; }
        public string Tanim { get; set; }
    }
    public class TarifeFiyat
    {
        public int Id { get; set; }
        public int TanimId { get; set; }
        public decimal NormalFiyat { get; set; }
        public decimal ZamliFiyat { get; set; }
        public decimal DagitimBedeli { get; set; }
        public decimal ReaktifBedel { get; set; }
        public DateTime BaşlamaTarihi { get; set; }
        public DateTime BitişTarihi { get; set; }
        public EnumTerim Terim { get; set; }
        public EnumAgOg AgOg { get; set; }
        public EnumEnerjiCinsi EnerjiCinsi { get; set; }
    }

    public class Vergiler
    {
        public string Adi { get; set; }
        public decimal Deger { get; set; }
    }
}
