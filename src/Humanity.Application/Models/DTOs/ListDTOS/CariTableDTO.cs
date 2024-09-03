using Humanity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Humanity.Application.Models.DTOs.ListDTOS
{
    public class CariTableDTO
    {
        public CariTableDTO() { }

        public CariTableDTO(CariKart cari)
        {
            Id = cari.Id;
            Adi=cari.Adi;
            Soyadi=cari.Soyadi;
            Unvan=cari.Unvan;
            Tckn = cari.Tckn;
            Vkn = cari.Vkn;
        }

        public int Id { get; set; }
        public string Adi { get; set; }
        public string Soyadi { get; set; }
        public string? Unvan { get; set; }

        public long? Tckn { get; set; }
        public long? Vkn { get; set; }


    }
}
