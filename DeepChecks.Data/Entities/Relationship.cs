using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeepChecks.Data.Entities
{
    public class Relationship
    {
        [Key]
        public int RelationshipId { get; set; }
        [Required]
        public string RelationshipName { get; set; }
        [Required]
        public Guid OwnerId { get; set; }

        [ForeignKey(nameof(Participant))]
        public int ParticipantId { get; set; }
        public virtual Participant Participant { get; set; }
        
        [ForeignKey(nameof(Check))]
        public int CheckId { get; set; }
        public virtual Check Check { get; set; }
    }
}
