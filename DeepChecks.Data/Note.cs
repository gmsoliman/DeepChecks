using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeepChecks.Data
{
    public class Note
    {
        [Key]
        public int NoteId { get; set; }
        public string NoteTitle { get; set; }
        [Required]
        public string NoteContent { get; set; }

        [ForeignKey(nameof(Check))]
        public int CheckId { get; set; }
        public virtual Check Check { get; set; }

        [ForeignKey(nameof(Participant))]
        public int ParticipantId { get; set; }
        public virtual Participant Participant { get; set; }
    }
}
