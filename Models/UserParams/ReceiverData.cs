using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace NumberGo.Models.UserParams
{
    public class ReceiverData
    {
        [Required(ErrorMessage = "Name not allowed white space.")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "Name length range need in 1 ~ 50.")]
        [RegularExpression(@"^[^\d]{1,}$", ErrorMessage = "Name not allowed have number.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Tel not allowed white space.")]
        [StringLength(18, MinimumLength = 7, ErrorMessage = "Tel length range need in 1 ~ 30.")]
        [RegularExpression(@"^[\d-]{1,}$", ErrorMessage = "Tel only have number or hyphen.")]
        public string Tel { get; set; }
    }
}