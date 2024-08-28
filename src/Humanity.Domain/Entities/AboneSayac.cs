using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Humanity.Domain.Entities
{
    public class AboneSayac
    {
        public int Id { get; set; }
        public int MusteriId { get; set; }
        public long SayacNo { get; set; }
        public string Marka { get; set; }
        public int FazAdedi { get; set; }
    }
}
