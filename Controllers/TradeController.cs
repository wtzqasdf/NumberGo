using System;
using System.Net;   
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
using NumberGo.Models.Tables;

namespace NumberGo.Controllers
{
    public class TradeController : BaseController
    {
        const string PASSWORD = "NumberGo";
        const string WEBNO = "A127924706";
        UserRepository _userRepo;
        OrderRepository _orderRepo;
        public TradeController(UserContext userContext, OrderContext orderContext)
        {
            _userRepo = new UserRepository(userContext);
            _orderRepo = new OrderRepository(orderContext);
        }

        [HttpPost]
        [LoginCheck]
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
            CreateTradeForm form = new CreateTradeForm(TradeEnvironment.Product);
            string orderNo = OrderHelper.GenerateOrderNumber();
            int price = 60;
            //先將訂單資料預先存到DB
            _orderRepo.AddOrder(orderNo, userAccount, price);
            //不會變動的
            form.Password = PASSWORD;
            form.WebNo = WEBNO;
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

        public IActionResult ReceiveTradeResult(ReceiveTradeData data)
        {
            //驗證是否為金流端傳回來的
            if (!data.IsVaild(WEBNO, PASSWORD))
            {
                return Redirect("/");
            }
            //驗證交易是否成功
            if (!data.TradeIsPass())
            {
                _orderRepo.UpdateOrder(data.OrderNo, data.BuysafeNo, data.pan_no4, data.IsForeignCard(), false, WebUtility.UrlDecode(data.ErrDesc));
                return Redirect("/");
            }
            Order oldOrder = _orderRepo.GetOrder(data.OrderNo);
            //更新相關帳號的付費權限
            _userRepo.UpdatePremium(oldOrder.Account);
            //更新訂單狀態
            _orderRepo.UpdateOrder(data.OrderNo, data.BuysafeNo, data.pan_no4, data.IsForeignCard(), true);
            return Redirect("/");
        }
    }
}