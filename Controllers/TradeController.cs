using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using NumberGo.Models.UserParams;
using NumberGo.Auth;
using NumberGo.PayNow;

namespace NumberGo.Controllers
{
    public class TradeController : BaseController
    {
        const string PASSWORD = "NumberGo";
        
        [LoginCheck]
        [HttpPost]
        public IActionResult Create(ReceiverData data)
        {
            if (!ModelState.IsValid)
            {
                return Json(false, errors: GetModelErrors());
            }
            //檢查是否為付費會員
            //從資料庫抓取該使用者的Receiver的ID與Email都填入Email
            CreateTradeForm form = new CreateTradeForm(TradeEnvironment.Test);
            //不太會變動的
            form.Password = PASSWORD;
            form.WebNo = "A127924706";
            form.ECPlatform = "NumberGo";
            form.OrderInfo = "NumberGo upgrade account.";
            form.TotalPrice = 100;
            //會經常變動的
            form.OrderNo = "A123456A789";
            form.ReceiverEmail = "abc@gmail.com";
            form.ReceiverID = "0981673956";
            form.ReceiverName = data.Name;
            form.ReceiverTel = data.Tel;
            return Json(true, obj: form);
        }

        [HttpPost]
        public IActionResult ReceiveTradeResult(ReceiveTradeData data)
        {
            //驗證是否為金流端傳回來的
            if (!data.IsVaild(PASSWORD))
            {
                return BadRequest();
            }
            //驗證交易是否成功
            if (data.TradeIsPass())
            {
                //資料庫邏輯
            }
            else
            {
                //資料庫邏輯
            }
            return Ok();
        }
    }
}