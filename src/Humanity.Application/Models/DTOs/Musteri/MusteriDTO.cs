using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Humanity.Application.Models.DTOs.Musteri
{
    public class MusteriDTO
    {
        private Domain.Entities.Musteri musteri;

        public MusteriDTO(Domain.Entities.Musteri musteri)
        {
            this.musteri = musteri;
        }
    }
}
