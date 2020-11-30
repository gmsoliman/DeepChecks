using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeepChecks.Data
{
    public class Relationship
    {
        [Key]
        public int RelationshipId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public Guid OwnerId { get; set; }

        public virtual ICollection<Check> Checks { get; set; } = new List<Check>();
    }
}
