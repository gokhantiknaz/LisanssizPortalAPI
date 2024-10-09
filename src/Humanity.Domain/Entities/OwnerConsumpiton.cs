using Humanity.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Humanity.Domain.Entities
{
    public class OwnerConsumpiton:BaseEntity
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "jsonb")]
        public string Json { get; set; }
        public long SerNo { get; set; }

        public string Donem { get; set; }

        public string Firma { get; set; }
    }
}
