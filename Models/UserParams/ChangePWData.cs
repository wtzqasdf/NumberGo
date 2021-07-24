using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace NumberGo.Models.UserParams
{
    public class ChangePWData
    {
        [Required(ErrorMessage = "Old password not allowed white space.")]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "Old paassword length range need in 6 ~ 30.")]
        [RegularExpression("^[a-zA-Z0-9]{1,}$", ErrorMessage = "Old paassword only have english and number.")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "New password not allowed white space.")]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "New password length range need in 6 ~ 30.")]
        [RegularExpression("^[a-zA-Z0-9]{1,}$", ErrorMessage = "New password only have english and number.")]
        public string NewPassword { get; set; }
    }
}
