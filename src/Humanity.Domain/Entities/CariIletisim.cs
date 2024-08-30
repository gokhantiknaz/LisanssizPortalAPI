using Humanity.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Humanity.Domain.Entities
{
    public class CariIletisim : BaseEntity, IAuditableEntity, ISoftDeleteEntity
    {
        [ForeignKey(nameof(Iletisim))]
        public int IletisimId { get; set; }

        [ForeignKey("IletisimId")]
        public Iletisim Iletisim { get; set; }

        [ForeignKey(nameof(CariKart))]
        public int CariKartId { get; set; }

        [ForeignKey("CariKartId")]
        public CariKart CariKart{ get; set; }
        
        public Guid CreatedBy { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public Guid? LastModifiedBy { get; set; }
        public DateTimeOffset? LastModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
    }
}
