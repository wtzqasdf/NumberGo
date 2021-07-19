using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace NumberGo.Models.UserParams
{
    public class LoginData
    {
        [Required(ErrorMessage = "Account not allowed white space.")]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "Account length range need in 6 ~ 30.")]
        [RegularExpression("^[a-zA-Z0-9]{1,}$", ErrorMessage = "Account only have english and number.")]
        public string Account { get; set; }

        [Required(ErrorMessage = "Password not allowed white space.")]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "Password length range need in 6 ~ 30.")]
        [RegularExpression("^[a-zA-Z0-9]{1,}$", ErrorMessage = "Password only have english and number.")]
        public string Password { get; set; }
    }
}
