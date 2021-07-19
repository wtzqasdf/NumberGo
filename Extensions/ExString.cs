using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NumberGo.Extensions
{
    public static class ExString
    {
        /// <summary>
        /// 隱藏後半部的字元
        /// </summary>
        /// <param name="sp">要替換的字元</param>
        /// <returns></returns>
        public static string HideHalfOfEnd(this string str, char sp = '*')
        {
            int halfLength = 0;
            if (str.Length > 1)
            {
                halfLength = str.Length / 2;
            }
            string startStr = str.Substring(0, str.Length - halfLength);
            return startStr.PadRight(str.Length, sp);
        }
    }
}
