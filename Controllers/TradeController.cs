using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using NumberGo.Models.UserParams;
using NumberGo.Auth;
using NumberGo.PayNow;
using NumberGo.Utils;
using NumberGo.Models.Repositories;
using NumberGo.Models.Contexts;

namespace NumberGo.Controllers
{
    public class TradeController : BaseController
    {
        const string PASSWORD = "NumberGo";
        UserRepository _userRepo;
        OrderRepository _orderRepo;
        public TradeController(UserContext userContext, OrderContext orderContext)
        {
            _userRepo = new UserRepository(userContext);
            _orderRepo = new OrderRepository(orderContext);
        }

        [LoginCheck]
        [HttpPost]
        public IActionResult Create(ReceiverData data)
        {
            if (!ModelState.IsValid)
            {
                return Json(false, errors: GetModelErrors());
            }
            string userAccount = GetSessionValue("account");
            Models.Tables.User user = _userRepo.GetUser(userAccount);
            //如果已經是付費會員的話就跳過
            if (user.IsPremium)
            {
                return Json(false, msg: "You has been premium member.");
            }
            CreateTradeForm form = new CreateTradeForm(TradeEnvironment.Test);
            string orderNo = OrderHelper.GenerateOrderNumber();
            int price = 100;
            //先將訂單資料預先存到DB
            _orderRepo.AddOrder(orderNo, userAccount, price);
            //不會變動的
            form.Password = PASSWORD;
            form.WebNo = "A127924706";
            form.ECPlatform = "NumberGo";
            form.OrderInfo = "NumberGo upgrade account.";
            form.TotalPrice = price;
            //會經常變動的
            form.OrderNo = orderNo;
            form.ReceiverEmail = user.Email;
            form.ReceiverID = user.Email;
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