using RetoTecnico.Application.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetoTecnico.Application.DTOs
{
    public class RegisterModel
    {
        [Required]
        [ValidEmail(ErrorMessage = "Por favor ingrese un email válido.")]
        public string Email { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 8)]
        public string Password { get; set; }
    }
}
