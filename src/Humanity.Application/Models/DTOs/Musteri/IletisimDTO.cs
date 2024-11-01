using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Humanity.Application.Models.DTOs.Musteri
{
    public class IletisimDTO
    {
        public int? Id { get; set; }
        public string? CepTel { get; set; }
        public string? Email { get; set; }
        public int? Ilid { get; set; }
        public int? Ilceid { get; set; }
        public string? Adres { get; set; }
    }
}
