﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeepChecks.Models.Category
{
    public class CategoryListItem
    {
        public int CategoryId { get; set; }
        [Display(Name="Type of Entry")]
        public string CategoryTitle { get; set; }
    }
}
