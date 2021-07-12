using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proiect.Models.MyModels
{
    public class CheckBoxViewModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Checked { get; set; }
    }
}