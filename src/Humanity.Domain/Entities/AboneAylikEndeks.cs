using Humanity.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Humanity.Domain.Entities
{
    public class AboneAylikEndeks : BaseEntity, ISoftDeleteEntity
    {
        public int Id { get; set; }

        public int AboneId { get; set; }

        [ForeignKey(nameof(AboneId))]
        public Abone Abone { get; set; }

        public string Donem { get; set; }

        public decimal TotalTuketimCekis { get; set; }

        public decimal TotalUretimVeris { get; set; }

        public decimal TotalReakIndVeris { get; set; }

        public decimal TotalReakKapVeris { get; set; }

        public decimal TotalReakIndCekis { get; set; }

        public decimal TotalReakKapCekis { get; set; }

        public decimal Carpan { get; set; }

        public Guid CreatedBy { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public Guid? LastModifiedBy { get; set; }
        public DateTimeOffset? LastModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
    }
}
