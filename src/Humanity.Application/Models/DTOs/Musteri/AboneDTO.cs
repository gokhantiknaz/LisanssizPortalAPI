using Humanity.Domain.Entities;
using System;
using System.Collections.Generic;
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
        public UreticiLisans LisansBilgileri { get; set; }
        public AboneSayac? AboneSayac { get; set; }
        public AboneIletisimDTO AboneIletisim { get; set; }
    }
}
