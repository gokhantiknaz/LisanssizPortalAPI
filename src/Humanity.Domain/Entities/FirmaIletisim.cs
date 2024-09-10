using Humanity.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Humanity.Domain.Entities
{
    public class FirmaIletisim : BaseEntity, IAuditableEntity, ISoftDeleteEntity
    {
        [ForeignKey(nameof(Iletisim))]
        public int IletisimId { get; set; }

        [ForeignKey("IletisimId")]
        public Iletisim Iletisim { get; set; }

        [ForeignKey(nameof(Firma))]
        public int FirmaId { get; set; }

        [ForeignKey("FirmaId")]
        public Firma Firma { get; set; }

        public Guid CreatedBy { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public Guid? LastModifiedBy { get; set; }
        public DateTimeOffset? LastModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
    }
}
