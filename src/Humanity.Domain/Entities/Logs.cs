using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Humanity.Domain.Entities
{
    public class Logs
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [AllowNull]
        public string Application { get; set; }

        public DateTime Timestamp { get; set; }

        [AllowNull]
        public string Level { get; set; }

        public string Message { get; set; }


        public string Logger { get; set; }

        public string Callsite { get; set; }
        public string Exception { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
