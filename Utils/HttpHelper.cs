using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Collections.Specialized;
using System.Text.RegularExpressions;

namespace NumberGo.Utils
{
    public static class HttpHelper
    {
        /// <summary>
        /// 傳送一個POST請求。
        /// </summary>
        /// <param name="url">要請求的網址。</param>
        /// <param name="parameters">POST參數。</param>
        /// <returns>返回請求結果。</returns>
        public static string CreatePostRequest(string url, NameValueCollection parameters = null)
        {
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            if (parameters != null)
            {
                using Stream reqStream = req.GetRequestStream();
                using StreamWriter reqWriter = new StreamWriter(reqStream);
                reqWriter.Write(parameters.ToString());
                reqWriter.Close();
            }

            HttpWebResponse res = (HttpWebResponse)req.GetResponse();
            using Stream stream = res.GetResponseStream();
            using StreamReader reader = new StreamReader(stream);
            string html = reader.ReadToEnd();

            return html;
        }

        public static bool IsCrawler(string userAgent)
        {
            return Regex.IsMatch(userAgent, @"bot|crawler|baiduspider|80legs|ia_archiver|voyager|curl|wget|yahoo! slurp|mediapartners-google", RegexOptions.IgnoreCase);
        }

    }
}
