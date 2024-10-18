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
    public class MusteriEntegrasyon : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Musteri")]
        public int MusteriId { get; set; }
        
        [ForeignKey("MusteriId")]
        public Musteri Musteri { get; set; }

        public int DagitimFirmaId { get; set; }

        public int ServisId { get; set; }

        public string KullaniciAdi { get; set; }

        public string Sifre { get; set; }

        public string ServisAdres { get; set; }

    }
}
