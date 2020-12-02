using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeepChecks.Data.Entities
{
    public class Entry
    {
        [Key]
        public int EntryId { get; set; }
        [Required]
        public string EntryContent { get; set; }
        [Required]
        public Guid OwnerId { get; set; }

        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        [ForeignKey(nameof(Participant))]
        public int ParticipantId { get; set; }
        public virtual Participant Participant { get; set; }
    }
}
