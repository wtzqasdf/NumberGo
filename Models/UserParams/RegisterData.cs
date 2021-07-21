using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace NumberGo.Models.UserParams
{
    public class RegisterData
    {
        [Required(ErrorMessage = "Account not allowed white space.")]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "Account length range need in 6 ~ 30.")]
        [RegularExpression("^[a-zA-Z0-9]{1,}$", ErrorMessage = "Account only have english and number.")]
        public string Account { get; set; }

        [Required(ErrorMessage = "Password not allowed white space.")]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "Password length range need in 6 ~ 30.")]
        [RegularExpression("^[a-zA-Z0-9]{1,}$", ErrorMessage = "Password only have english and number.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm password not allowed white space.")]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "Confirm password length range need in 6 ~ 30.")]
        [RegularExpression("^[a-zA-Z0-9]{1,}$", ErrorMessage = "Confirm password only have english and number.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Email not allowed white space.")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Email length too long or too short.")]
        [EmailAddress(ErrorMessage = "Email format incorrect.")]
        public string Email { get; set; }
    }
}
