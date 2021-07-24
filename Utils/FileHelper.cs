using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace NumberGo.Utils
{
    public static class FileHelper
    {
        public static async Task<byte[]> ReadToBytes(string file) 
        {
            return await Task.Run(() => {
                using FileStream fs = new FileStream(file, FileMode.Open);
                byte[] bytes = new byte[fs.Length];
                fs.Read(bytes, 0, bytes.Length);
                fs.Close();
                return bytes;
            });
        }
    }
}
