using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Humanity.Application.Models.Requests
{
    public class SaatlikEndeksRequest
    {

        public int? MusteriId { get; set; }
        public string? ProfilTarihi { get; set; }
        public decimal? TuketimCekis { get; set; }
        public decimal? ReakIndCekis { get; set; }
        public decimal? ReakKapCekis { get; set; }

        public decimal? UretimVeris { get; set; }

        public decimal? ReakIndVeris { get; set; }

        public decimal? ReakKapVeris { get; set; }

        public decimal? Carpan { get; set; }

        public string? Donem { get; set; }
    }
}
