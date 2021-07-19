using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace NumberGo.Models.UserParams
{
    public class ForgotPWData
    {
        [Required(ErrorMessage = "Account not allowed white space.")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Account length range need in 6 ~ 20.")]
        [RegularExpression("^[a-zA-Z0-9]{1,}$", ErrorMessage = "Account only have english and number.")]
        public string Account { get; set; }

        [Required(ErrorMessage = "Email not allowed white space.")]
        [StringLength(200, MinimumLength = 6, ErrorMessage = "Email length too long or too short.")]
        [EmailAddress(ErrorMessage = "Email format incorrect.")]
        public string Email { get; set; }
    }
}
