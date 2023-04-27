using System.ComponentModel.DataAnnotations;

namespace Application.DTO
{
    public class LoginUserDTO
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9]+$")]
        public string Name { get; set; }
        [Required]
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^\w\s]).{8,}$")]
        public string Password { get; set; }
    }
}
