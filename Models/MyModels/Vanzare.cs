using Proiect.Models.MyValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proiect.Models.MyModels
{
    public class Vanzare
    {
        [Key]
        public int Vanzare_id { get; set; }

        //[NameValidator]
        [MinLength(2, ErrorMessage = "Seller name cannot be less than 2!"),
        MaxLength(30, ErrorMessage = "Seller name cannot be more than 30!")]
        public string name { get; set; }

        // one to many
        public virtual ICollection<Item> Items { get; set; }

        [NotMapped]
        public IEnumerable<SelectListItem> SellerList { get; set; }
    }
}