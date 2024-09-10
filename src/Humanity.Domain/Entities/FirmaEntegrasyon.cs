using Humanity.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Humanity.Domain.Entities
{
    public class FirmaEntegrasyon : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Firma")]
        public int FirmaId { get; set; }
        
        [ForeignKey("FirmaId")]
        public Firma Firma { get; set; }

        public int ServisId { get; set; }

        public string ServisKullaniciAdi { get; set; }

        public string ServisSifre { get; set; }

        public string ServisAdres { get; set; }

    }
}
