using Microsoft.WindowsAzure.Storage;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proiect.Models.MyModels
{
    public class Client
    {
        [Key]
        public int Client_id { get; set; }

        //[NameValidator]
        public string name { get; set; }
    }
}