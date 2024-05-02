using RetoTecnico.Application.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetoTecnico.Application.DTOs
{
    public class LoginModel
    {
        [Required]
        [ValidEmail(ErrorMessage = "Por favor ingrese un email válido.")]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
