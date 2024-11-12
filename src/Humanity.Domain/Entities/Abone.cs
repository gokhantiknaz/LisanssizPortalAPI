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
    public class Abone : BaseEntity, IAuditableEntity, ISoftDeleteEntity
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("MusteriId")]
        public Musteri Musteri { get; set; }

        public int MusteriId { get; set; }

        public string? Adi { get; set; }
        public string? Soyadi { get; set; }
        public string? Unvan { get; set; }

        public long? Tckn { get; set; }
        public long? Vkn { get; set; }

        public int? Tarife { get; set; }
        public string? EtsoKodu { get; set; }
        public int? DagitimFirmaId { get; set; }
        public long? SeriNo { get; set; }
        public double? SozlesmeGucu { get; set; }
        public double BaglantiGucu { get; set; }
        public double? KuruluGuc { get; set; }
        public SahisTip SahisTip { get; set; }
        public int Terim { get; set; }
        public int Agog { get; set; }

        public int? OzelkodId1 { get; set; }
        public int? OzelkodId2 { get; set; }
        public int? OzelkodId3 { get; set; }

        public Guid CreatedBy { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public Guid? LastModifiedBy { get; set; }
        public DateTimeOffset? LastModifiedOn { get; set; }

        public AboneIletisim AboneIletisim { get; set; }
        public bool MahsubaDahil { get; set; }
        public Status Durum { get; set; }
        public bool IsDeleted { get; set; }

        public decimal Carpan { get; set; }
    }

}
