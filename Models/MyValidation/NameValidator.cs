using Proiect.Models.MyModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proiect.Models.MyValidation
{
    public class NameValidator : ValidationAttribute
    {
        
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var item_info = (Item)validationContext.ObjectInstance;
            string name = item_info.name;
            bool cond = true;
            foreach (char c in name)
            {
                if (Char.IsLetterOrDigit(c) == false && !c.Equals(' ') && !c.Equals('-'))
                {
                    cond = false;
                    break;
                }
            }
            return cond ? ValidationResult.Success : new ValidationResult("This name is invalid!");
        }
        
    }
}