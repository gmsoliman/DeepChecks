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
    public class Relationship
    {
        [Key]
        public int RelationshipId { get; set; }
        [Required]
        [DisplayName("Relationship Name")]
        public string RelationshipName { get; set; }
        [Required]
        public Guid OwnerId { get; set; }

        //public virtual ICollection<Participant> Participants { get; set; } = new List<Participant>();
        //public Relationship()
        //{
        //    this.Participants = new HashSet<Participant>();
        //}
        public virtual ICollection<Check> Checks { get; set; } = new List<Check>();

    }
}
