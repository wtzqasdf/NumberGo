using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;

namespace NumberGo.PayNow
{
    public class TradeReceiveData
    {
        /// <summary>
        /// 商家帳號
        /// </summary>
        public string WebNo { get; set; }
        /// <summary>
        /// PayNow的訂單編號
        /// </summary>
        public string BuysafeNo { get; set; }
        /// <summary>
        /// 驗證碼，使用IsVaild方法驗證
        /// </summary>
        public string Passcode { get; set; }
        /// <summary>
        /// 商家自訂編號
        /// </summary>
        public string OrderNo { get; set; }
        /// <summary>
        /// 交易結果，請使用TradeIsPass方法驗證
        /// </summary>
        public string TranStatus { get; set; }
        /// <summary>
        /// 交易錯誤描述
        /// </summary>
        public string ErrDes { get; set; }
        /// <summary>
        /// 交易金額
        /// </summary>
        public int TotalPrice { get; set; }
        /// <summary>
        /// 付款方式
        /// </summary>
        public string PayType { get; set; }
        /// <summary>
        /// 信用卡末四碼
        /// </summary>
        public string pan_no4 { get; set; }
        /// <summary>
        /// 是否為國外卡，請使用IsForeignCard方法驗證
        /// </summary>
        public int Card_Foreign { get; set; }

        public bool IsVaild(string password)
        {
            SHA1 sha = SHA1.Create();
            byte[] bytes = Encoding.ASCII.GetBytes(WebNo + OrderNo + TotalPrice + password + TranStatus);
            byte[] hashBytes = sha.ComputeHash(bytes, 0, bytes.Length);
            return BitConverter.ToString(hashBytes) == Passcode;
        }

        public bool TradeIsPass()
        {
            return TranStatus == "S";
        }

        public bool IsForeignCard()
        {
            return Card_Foreign == 1;
        }
    }
}
