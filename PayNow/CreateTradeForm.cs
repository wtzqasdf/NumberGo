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
        /// 付款方式，預設01信用卡
        /// </summary>
        public string PayType { get; set; }
        /// <summary>
        /// 交易頁面語系，預設0為中文、1為英文
        /// </summary>
        public string PayEN { get; set; }
        public int EPT { get { return 1; } }
        public string PassCode { get { return GetPassCode(WebNo, OrderNo, TotalPrice, Password); } }
        public string TradeUrl { get; private set; }

        public CreateTradeForm(TradeEnvironment envi)
        {
            PayType = "01";
            PayEN = "0";
            if (envi == TradeEnvironment.Product)
            {
                TradeUrl = "https://www.paynow.com.tw/service/etopm.aspx";
            }
            else
            {
                TradeUrl = "https://test.paynow.com.tw/service/etopm.aspx";
            }
        }

        private string GetPassCode(string webNo, string orderNo, int totalPrice, string password)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(webNo + orderNo + totalPrice + password);
            byte[] hashBytes = SHA1.Create().ComputeHash(bytes, 0, bytes.Length);
            return BitConverter.ToString(hashBytes).Replace("-", "");
        }
    }
}
