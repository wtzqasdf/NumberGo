using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NumberGo.Utils
{
    public static class OrderHelper
    {
        /// <summary>
        /// 產生一組訂單號碼
        /// </summary>
        /// <returns></returns>
        public static string GenerateOrderNumber()
        {
             return string.Format("NGO{0}", DateTime.Now.ToString("yyyyMMddHHmmssfff"));
        }
    }
}
