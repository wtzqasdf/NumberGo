using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Newtonsoft.Json;

namespace NumberGo.PayNow
{
    public class CreateTradeForm
    {
        /// <summary>
        /// 商家交易碼
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 網站帳號，可為統編或身分證
        /// </summary>
        public string WebNo { get; set; }
        /// <summary>
        /// 買家姓名，不可為數字
        /// </summary>
        public string ReceiverName { get; set; }
        /// <summary>
        /// 買家ID，為英數字，可為身分證、Email、手機電話
        /// </summary>
        public string ReceiverID { get; set; }
        /// <summary>
        /// 消費者電話，手機或室內電話
        /// </summary>
        public string ReceiverTel { get; set; }
        /// <summary>
        /// 消費者Email
        /// </summary>
        public string ReceiverEmail { get; set; }
        /// <summary>
        /// 訂單編號
        /// </summary>
        public string OrderNo { get; set; }
        /// <summary>
        /// 商家平台名稱
        /// </summary>
        public string ECPlatform { get; set; }
        /// <summary>
        /// 總金額
        /// </summary>
        public int TotalPrice { get; set; }
        /// <summary>
        /// 訂單內容
        /// </summary>
        public string OrderInfo { get; set; }
        /// <summary>
        /// 付款方式
        /// </summary>
        public PayTypes PayType { get; set; }
        /// <summary>
        /// 交易頁面語系
        /// </summary>
        public PayLanguage PayEN { get; set; }

        private int _EPT;
        private string _reqUrl;

        public CreateTradeForm(TradeEnvironment envi)
        {
            PayType = PayTypes.Credit;
            PayEN = PayLanguage.Chinese;
            _EPT = 1;
            if (envi == TradeEnvironment.Product)
            {
                _reqUrl = "https://www.paynow.com.tw/service/etopm.aspx";
            }
            else 
            {
                _reqUrl = "https://test.paynow.com.tw/service/etopm.aspx";
            }
        }

        public string ToHtml()
        {
            string passCode = GetPassCode(WebNo, OrderNo, TotalPrice, Password);
            string payType = PayType == PayTypes.Credit ? "01" : "02";
            string payEN = PayEN == PayLanguage.Chinese ? "0" : "1";
            StringBuilder html = new StringBuilder();
            html.Append($"<form method=\"POST\" action=\"{_reqUrl}\">");
            html.Append($"<input type=\"hidden\" name=\"WebNo\" value=\"{WebNo}\">");
            html.Append($"<input type=\"hidden\" name=\"PassCode\" value=\"{passCode}\">");
            html.Append($"<input type=\"hidden\" name=\"OrderNo\" value=\"{OrderNo}\">");
            html.Append($"<input type=\"hidden\" name=\"ECPlatform\" value=\"{ECPlatform}\">");
            html.Append($"<input type=\"hidden\" name=\"TotalPrice\" value=\"{TotalPrice}\">");
            html.Append($"<input type=\"hidden\" name=\"OrderInfo\" value=\"{OrderInfo}\">");
            html.Append($"<input type=\"hidden\" name=\"ReceiverTel\" value=\"{ReceiverTel}\">");
            html.Append($"<input type=\"hidden\" name=\"ReceiverName\" value=\"{ReceiverName}\">");
            html.Append($"<input type=\"hidden\" name=\"ReceiverEmail\" value=\"{ReceiverEmail}\">");
            html.Append($"<input type=\"hidden\" name=\"ReceiverID\" value=\"{ReceiverID}\">");
            html.Append($"<input type=\"hidden\" name=\"PayType\" value=\"{payType}\">");
            html.Append($"<input type=\"hidden\" name=\"PayEN\" value=\"{payEN}\">");
            html.Append($"<input type=\"hidden\" name=\"EPT\" value=\"{_EPT}\">");
            html.Append("</form>");

            html.Append("<script>");
            html.Append("document.getElementsByTagName('form')[0].submit();");
            html.Append("</script>");
            return html.ToString();
        }

        private string GetPassCode(string webNo, string orderNo, int totalPrice, string password)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(webNo + orderNo + totalPrice + password);
            byte[] hashBytes = SHA1.Create().ComputeHash(bytes, 0, bytes.Length);
            return BitConverter.ToString(hashBytes).Replace("-", "");
        }
    }
}
