using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Humanity.Application.Models.Responses
{
    public class EndeksResponses
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

        public int? TotalRows { get; set; }

    }

    public class AylikEndeksRes
    {
        public int Id { get; set; }

        public int AboneId { get; set; }

        public int EndexYear { get; set; }
        public int EndexMonth { get; set; }
        public string Donem { get; set; }
        public long EndexDate { get; set; }
        public double T1Endex { get; set; }
        public double T2Endex { get; set; }
        public double T3Endex { get; set; }
        public double T4Endex { get; set; }

        public double ReactiveCapasitive { get; set; }
        public double ReactiveInductive { get; set; }

    }
}
