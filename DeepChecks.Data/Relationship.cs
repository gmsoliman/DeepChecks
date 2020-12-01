using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeepChecks.Data
{
    public class Relationship
    {
        [Key, Column(Order = 0)]
        [ForeignKey(nameof(Participant))]
        public int ParticipantId { get; set; }
        public virtual Participant Participant { get; set; }
        
        [Key, Column(Order = 1)]
        [ForeignKey(nameof(Check))]
        public int CheckId { get; set; }
        public virtual Check Check { get; set; }

        public string RelationshipName { get; set; }
    }
}
