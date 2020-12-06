using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeepChecks.Data.Entities
{
    public class Participant
    {
        [Key]
        public int ParticipantId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        //[DisplayName("Full Name")]
        //public string FullName => $"{FirstName} {LastName}";
        [Required]
        public Guid OwnerId { get; set; }

        [ForeignKey(nameof(Check))]
        public int CheckId { get; set; }
        public virtual Check Check { get; set; }

        //public virtual ICollection<Relationship> Relationships { get; set; } = new List<Relationship>();
        //public Participant()
        //{
        //    this.Relationships = new HashSet<Relationship>();
        //}
        //public virtual ICollection<Note> Notes { get; set; } = new List<Note>();
        public virtual ICollection<Entry> Entries { get; set; } = new List<Entry>();
    }
}
