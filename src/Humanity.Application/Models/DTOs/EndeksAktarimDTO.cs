using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Humanity.Application.Models.DTOs
{
    public class EndeksAktarimDTO
    {

        
        public int Id { get; set; }

        public int MusteriId { get; set; }
        public DateTime ProfilDate { get; set; }

        public decimal CekisTuketim { get; set; }

        public decimal CekisReaktifInduktif { get; set; }

        public decimal CekisReaktifKapasitif { get; set; }

        public decimal Uretim { get; set; }

        public decimal VerisReaktifInduktif { get; set; }

        public decimal VerisReaktifKapasitif { get; set; }

        public decimal Carpan { get; set; }

        public string Donem { get; set; }

    }
}
