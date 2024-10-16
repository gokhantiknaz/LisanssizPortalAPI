using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Humanity.Domain.Entities
{
    public class AboneOzelKod
    {
        public int Id { get; set; }
        public int RootId { get; set; }
        public int KodNo { get; set; }
        public string Kod { get; set; }
        public string Ad { get; set; }
    }
}
