using Humanity.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Humanity.Domain.Entities
{
    public class MusteriSaatlikEndeks : BaseEntity
    {

        public int Id { get; set; }

        public int MusteriId { get; set; }

        [ForeignKey(nameof(MusteriId))]
        public Musteri Musteri { get; set; }

        public DateTimeOffset ProfilDate { get; set; }

        public decimal CekisTuketim { get; set; }

        public decimal CekisReaktifInduktif { get; set; }

        public decimal CekisReaktifKapasitif { get; set; }

        public decimal Uretim { get; set; }

        public decimal VerisReaktifInduktif { get; set; }

        public decimal VerisReaktifKapasitif { get; set; }

        public decimal Carpan { get; set; }

        public string Donem { get; set; }

        public Guid CreatedBy { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public Guid? LastModifiedBy { get; set; }
        public DateTimeOffset? LastModifiedOn { get; set; }


    }
}
