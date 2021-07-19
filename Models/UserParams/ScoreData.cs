using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using NumberGo.ValidateAttributes;

namespace NumberGo.Models.UserParams
{
    public class ScoreData
    {
        [Required(ErrorMessage = "NickName not allowed white space.")]
        [StringLength(30, MinimumLength = 1, ErrorMessage = "NickName length range need in 1 ~ 30.")]
        [RegularExpression("^[^<>'\"]{1,}$", ErrorMessage = "NickName not allowed special characters.")]
        public string NickName { get; set; }

        [Required(ErrorMessage = "Level not allowed white space.")]
        [Range(1, 10, ErrorMessage = "Level out of range.")]
        public short Level { get; set; }

        [Required(ErrorMessage = "Elapsed time not allowed white space.")]
        [Time(ErrorMessage = "Elapsed time format wrong.")]
        public TimeSpan ElapsedTime { get; set; }
    }
}
