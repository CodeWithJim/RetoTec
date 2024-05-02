using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RetoTecnico.Application.Validations
{
    public class ValidEmail:ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is string email)
            {
                return Regex.IsMatch(email, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            }
            return false;
        }
    }
}