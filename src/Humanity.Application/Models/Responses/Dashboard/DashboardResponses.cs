using Humanity.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Humanity.Application.Models.Responses.Dashboard
{
    public class AboneAylikTuketim:BaseEntity
    {
        public string Unvan { get; set; }
        public int EndexMonth { get; set; }
        public int EndexYear { get; set; }
        public double T1Usage { get; set; }
        public double T2Usage { get; set; }
        public double T3Usage { get; set; }
    }
}
