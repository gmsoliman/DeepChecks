﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeepChecks.Models.Entry
{
    public class EntryListItem
    {
        public int EntryId { get; set; }
        public string EntryContent { get; set; }
        public int CategoryId { get; set; }
        public int ParticipantId { get; set; }
    }
}
