﻿using Humanity.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Humanity.Domain.Entities
{
    public class Fatura:BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("AboneId")]
        public Abone Abone { get; set; }

        public int AboneId { get; set; }

        public string Donem{ get; set; }

        public Guid CreatedBy { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public Guid? LastModifiedBy { get; set; }
        public DateTimeOffset? LastModifiedOn { get; set; }
    }
}
