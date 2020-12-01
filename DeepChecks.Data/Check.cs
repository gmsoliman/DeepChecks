﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeepChecks.Data
{
    public class Check
    {
        [Key]
        public int CheckId { get; set; }
        [Required]
        public string CheckTitle { get; set; }
        [Required]
        public DateTimeOffset CheckDate { get; set; }
        [Required]
        public Guid OwnerId { get; set; }

        public virtual ICollection<Relationship> Relationships { get; set; } = new List<Relationship>();
        public virtual ICollection<Category> Categories { get; set; } = new List<Category>();
        public virtual ICollection<Note> Notes { get; set; } = new List<Note>();
    }
}
