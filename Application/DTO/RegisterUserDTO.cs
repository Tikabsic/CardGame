using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class RegisterUserDTO
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9]+$")]
        public string Name { get; set; }
        [Required]
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^\w\s]).{8,}$")]
        public string Password { get; set; }
        public string ConfirmPassowrd { get; set; }

    }
}
