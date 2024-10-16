using Humanity.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Humanity.Domain.Enums.Enums;

namespace Humanity.Domain.Entities
{
    public class AboneTuketici:BaseEntity,ISoftDeleteEntity
    {
        [Key]
        public int Id { get; set; }
        public int AboneId { get; set; }
        [ForeignKey("AboneId")]
        public Abone Abone { get; set; }
        public int UreticiAboneId { get; set; }
        [ForeignKey("UreticiAboneId")]
        public Abone UreticiAbone { get; set; }
        public DateTime BaslamaZamani { get; set; }
        public Status Durum { get; set; }
        public bool IsDeleted { get ; set ; }
    }
}
