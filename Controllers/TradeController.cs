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
        const string PASSWORD = "NumberGo2021";
        
        [LoginCheck]
        [HttpPost]
        public IAsyncResult Create(ReceiverData data)
        {
            if (!ModelState.IsValid)
            {
                return Json(false, errors: GetModelErrors());
            }
            //從資料庫抓取該使用者的Receiver的ID與Email都填入Email
            CreateTradeForm form = new CreateTradeForm(TradeEnvironment.Test);
            form.Password = PASSWORD;
            form.OrderNo = "A123456A789";
            form.OrderInfo = "Test Info";
            form.TotalPrice = 111;
            form.WebNo = "A127924706";
            form.ECPlatform = "NumberGo";
            form.ReceiverEmail = "abc@gmail.com";
            form.ReceiverID = "0981673926";
            form.ReceiverName = "Yang";
            form.ReceiverTel = "0981673926";
            form.PayType = PayNow.PayTypes.Credit;
            return Json(true, msg: form);
        }

        [HttpPost]
        public IAsyncResult ReceiveTradeResult(ReceiveTradeData data)
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