using Proiect.Models.MyValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proiect.Models.MyModels
{
    public class Category
    {
        [Key]
        public int category_id { get; set; }

        //[NameValidator]
        [MinLength(2, ErrorMessage = "Category name cannot be less than 2!"),
            MaxLength(70, ErrorMessage = "Category name cannot be more than 70!")]
        public string name { get; set; }

        //many to many
        public virtual ICollection<Item> Items { get; set; }
    }
}