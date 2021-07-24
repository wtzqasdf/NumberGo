using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NumberGo.Utils
{
    public static class StringHelper
    {
        public static string GeneratePassword() 
        {
            return Guid.NewGuid().ToString().Replace("-", "").Substring(0, 8);
        }
    }
}
