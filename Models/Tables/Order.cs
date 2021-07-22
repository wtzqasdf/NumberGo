using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NumberGo.Models.Tables
{
    public class Order
    {
        /// <summary>
        /// 訂單號碼
        /// </summary>
        [Key]
        [Column("orderno")]
        public string OrderNo { get; set; }
        /// <summary>
        /// 平台商的訂單號碼
        /// </summary>
        [Column("buysafeno")]
        public string BuysafeNo { get; set; }
        /// <summary>
        /// 訂單建立日期
        /// </summary>
        [Column("createdate")]
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// 買家帳號
        /// </summary>
        [Column("account")]
        public string Account { get; set; }
        /// <summary>
        /// 總價格
        /// </summary>
        [Column("price")]
        public ushort Price { get; set; }
        /// <summary>
        /// 買家卡片末四碼
        /// </summary>
        [Column("cardno4")]
        public string CardNo4 { get; set; }
        /// <summary>
        /// 是否為國外卡
        /// </summary>
        [Column("isforeign")]
        public bool? IsForeign { get; set; }

        [Column("iscompleted")]
        public bool IsCompleted { get; set; }

        [Column("errormsg")]
        public string ErrorMessage { get; set; }
    }
}
