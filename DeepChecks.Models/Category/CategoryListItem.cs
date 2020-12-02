using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeepChecks.Models.Category
{
    public class CategoryListItem
    {
        [Display(Name="Category")]
        public string CategoryTitle { get; set; }
        [Display(Name = "Description")]
        public string CategoryDescription { get; set; }
    }
}
