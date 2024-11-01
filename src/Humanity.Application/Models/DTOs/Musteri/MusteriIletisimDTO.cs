using Humanity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Humanity.Application.Models.DTOs.Musteri
{
    public class MusteriIletisimDTO
    {
        public int MusteriId { get; set; }
        public IletisimDTO Iletisim { get; set; }
    }
}
