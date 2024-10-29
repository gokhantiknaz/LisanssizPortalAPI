using Humanity.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Humanity.Domain.Entities
{
    public class AboneEndeks : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("AboneId")]
        public Abone Abone { get; set; }

        public int AboneId { get; set; }
        public int EndexYear { get; set; }
        public int EndexMonth { get; set; }
        public long EndexDate { get; set; }
        public int EndexType { get; set; }
        public double T1Endex { get; set; }
        public double T2Endex { get; set; }
        public double T3Endex { get; set; }
        public double T4Endex { get; set; }
        public double ReactiveCapasitive { get; set; }
        public double ReactiveInductive { get; set; }
        public double MaxDemand { get; set; }
        public long DemandDate { get; set; }
        public double TSum { get; set; }

        public int SensorSerno { get; set; }
    }
}
