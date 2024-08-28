using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Humanity.Domain.Entities
{
    public class AboneSayac
    {
        [Key]
        public int Id { get; set; }
        
        [ForeignKey("Abone")]
        public int AboneId { get; set; }

        [ForeignKey("AboneId")]
        public Abone Abone { get; set; }

        public long SayacNo { get; set; }
        public string Marka { get; set; }
        public int FazAdedi { get; set; }
    }
}
