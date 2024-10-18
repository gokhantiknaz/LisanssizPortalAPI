using Humanity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Humanity.Application.Models.DTOs.firma
{
    public class FirmaIletisimDTO
    {

        private FirmaIletisim firmaIletisim;


        public FirmaIletisimDTO()
        {

        }

        public FirmaIletisimDTO(FirmaIletisim firmaIletisim)
        {
            this.firmaIletisim = firmaIletisim;
            IletisimId = firmaIletisim.IletisimId;
            FirmaId = firmaIletisim.FirmaId;
            CepTel = firmaIletisim.Iletisim.CepTel;
            Email = firmaIletisim.Iletisim.Email;
            Ilid = firmaIletisim.Iletisim.Ilid;
            Ilceid = firmaIletisim.Iletisim.Ilceid;
            Adres = firmaIletisim.Iletisim.Adres;
        }

        public int IletisimId { get; set; }

        public int FirmaId { get; set; }
        public string? CepTel { get; set; }
        public string? Email { get; set; }
        public int? Ilid { get; set; }
        public int? Ilceid { get; set; }
        public string? Adres { get; set; }
    }

}
