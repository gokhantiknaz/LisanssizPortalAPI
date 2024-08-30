using Humanity.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Humanity.Domain.Entities
{
    public class Iletisim:BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string? CepTel { get; set; }
        public string? Email { get; set; }
        public int? Ilid { get; set; }
        public int? Ilceid { get; set; }
        public string? Adres { get; set; }
    }
}
