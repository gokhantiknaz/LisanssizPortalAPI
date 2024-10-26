using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Humanity.Application.Models.Responses
{
    public class SaatlikEndeksRes
    {
        public int? AboneId { get; set; }
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

    public class AylikEndeksRes
    {
        public int Id { get; set; }

        public int AboneId { get; set; }

        public string Donem { get; set; }

        public decimal TotalTuketimCekis { get; set; }

        public decimal TotalUretimVeris { get; set; }

        public decimal TotalReakIndVeris { get; set; }

        public decimal TotalReakKapVeris { get; set; }

        public decimal TotalReakIndCekis { get; set; }

        public decimal TotalReakKapCekis { get; set; }

        public decimal Carpan { get; set; }

    }
}
