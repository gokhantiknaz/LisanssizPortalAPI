using Humanity.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Humanity.Domain.Entities
{
    public class EndeksVeriDurumu : BaseEntity
    {
        public int Id { get; set; }
        public DateTime Tarih { get; set; }
        public bool VeriCekildi { get; set; }
    }
}
