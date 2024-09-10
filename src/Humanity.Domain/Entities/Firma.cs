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
    public class Firma : BaseEntity, ISoftDeleteEntity, IAuditableEntity
    {
        [Key]
        public int Id { get; set; }
        public string FirmaAdi { get; set; }
        
        public string? FirmaUnvan { get; set; }

        public string VergiDairesi { get; set; }

        public long? Tckn { get; set; }
        public long? Vkn { get; set; }
        public Status Durum { get; set; }
        public GercekTuzel GercekTuzel { get; set; }

        public string SorumluAd { get; set; }
        public string SorumluSoyad { get; set; }
        public string SorumluTelefon { get; set; }
        public string SorumluEmail { get; set; }

        public int? OzelkodId1 { get; set; }
        public int? OzelkodId2 { get; set; }
        public int? OzelkodId3 { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public Guid? LastModifiedBy { get; set; }
        public DateTimeOffset? LastModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
 
    }
}
