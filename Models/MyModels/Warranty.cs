using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proiect.Models.MyModels
{
    public class Warranty
    {
        [Key]
        public int warranty_id { get; set; }

        //[RegularExpression(@"^^[a-zA-Z0-9 ]*$", ErrorMessage = "Duration must contain a number and 'month' word")]
        public string duration { get; set; }

        //one to many
        public virtual ICollection<Item> Items { get; set; }

        [NotMapped]
        public IEnumerable<SelectListItem> WarrantyList { get; set; }
    }
}