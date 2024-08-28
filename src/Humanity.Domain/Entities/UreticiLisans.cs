using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Humanity.Domain.Enums.Enums;

namespace Humanity.Domain.Entities
{
    public class UreticiLisans
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Musteri")]
        public int MusteriId { get; set; }
        public UretimSekli UretimSekli { get; set; }
        public LisansBilgisi LisansBilgisi { get; set; }
        public DateTime UretimBaslama { get; set; }
        public DateTime CagrimektupTarihi { get; set; }
        public int MahsupTipi { get; set; }
    }
}
