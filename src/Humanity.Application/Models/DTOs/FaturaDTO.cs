using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Humanity.Domain.Enums.Enums;

namespace Humanity.Application.Models.DTOs
{
    public class FaturaDTO
    {

        public int AboneId { get; set; }
        public string Donem { get; set; }
        public long SeriNo { get; set; }
        public decimal Fonlar { get; set; }
        public decimal KDV { get; set; }
        public List<FaturaDetayDTO> FaturaDetaylar { get; set; }

    }

    public class FaturaDetayDTO
    {
        public EnumThkKod KalemKod { get; set; }
        public string KalemAdi { get; set; }
        public decimal Kwh { get; set; }
        public decimal Tutar { get; set; }
        public decimal KdvOran { get; set; }
        public decimal KdvTuar { get; set; }
        public decimal BirimFiyat { get; internal set; }
    }

    public class CezaOranDTO
    {
        public double dKapasitifAltLimit { get; set; }
        public double dEnduktifCezaOrani { get; set; }
        public double dKapasitifCezaOrani { get; set; }
        public double dSekonderOran { get; set; }
        public double dEndKapDegLmtOrani { get; set; }
    }
}
