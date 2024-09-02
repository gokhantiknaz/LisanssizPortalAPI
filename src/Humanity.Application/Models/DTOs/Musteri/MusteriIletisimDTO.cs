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
        private MusteriIletisim musteriIletisim;


        public MusteriIletisimDTO()
        {
            
        }

        public MusteriIletisimDTO(MusteriIletisim musteriIletisim)
        {
            this.musteriIletisim = musteriIletisim;
            IletisimId = musteriIletisim.IletisimId;
            MusteriId= musteriIletisim.MusteriId;
            CepTel = musteriIletisim.Iletisim.CepTel;
            Email = musteriIletisim.Iletisim.Email;
            Ilid = musteriIletisim.Iletisim.Ilid;
            Ilceid = musteriIletisim.Iletisim.Ilceid;
            Adres = musteriIletisim.Iletisim.Adres;
        }

        public int IletisimId { get; set; }

        public int MusteriId { get; set; }
        public string? CepTel { get; set; }
        public string? Email { get; set; }
        public int? Ilid { get; set; }
        public int? Ilceid { get; set; }
        public string? Adres { get; set; }
    }
}
