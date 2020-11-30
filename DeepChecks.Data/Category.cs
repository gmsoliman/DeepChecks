using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeepChecks.Data
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required]
        public string CategoryTitle { get; set; }
        [Required]
        public string CategoryDescription { get; set; }

        [ForeignKey(nameof(Check))]
        public int CheckId { get; set; }
        public virtual Check Check { get; set; }

        public virtual ICollection<Entry> Entries { get; set; } = new List<Entry>();
    }
}
