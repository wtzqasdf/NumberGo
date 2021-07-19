using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;

namespace NumberGo.ValidateAttributes
{
    /// <summary>
    /// 驗證時間(時,分,秒,毫秒)屬性
    /// </summary>
    public class TimeAttribute : ValidationAttribute
    {

        public override bool IsValid(object value)
        {
            return Regex.IsMatch(value.ToString(), @"^[\d]{2,2}:[\d]{2,2}:[\d]{2,2}.[\d]{3,}$");
        }
    }
}
