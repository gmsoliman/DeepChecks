using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeepChecks.Models.Check
{
    public class CheckListItem
    {
        public int CheckId { get; set; }
        public string CheckTitle { get; set; }
        public DateTimeOffset CheckDate { get; set; }
    }
}
