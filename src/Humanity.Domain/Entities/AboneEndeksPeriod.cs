using Humanity.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Humanity.Domain.Enums.Enums;

namespace Humanity.Domain.Entities
{
    public class AboneEndeksPeriod : BaseEntity
    {
        public int Id { get; set; }

        [ForeignKey("AboneEndeksId ")]
        public AboneEndeks AboneEndeks { get; set; }

        public int AboneEndeksId { get; set; }

        public EnumEnerjiCinsi EnerijCinsi { get; set; }

        public EnunEneriTur EneriTur { get; set; }

        public EnumEndeksDirection EndeksDirection { get; set; }
        
        public double IlkEndeks { get; set; }

        public double SonEndeks { get; set; }

        public double Toplam { get; set; }


    }
}
