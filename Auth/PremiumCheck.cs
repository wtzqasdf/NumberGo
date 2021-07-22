using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using NumberGo.Models.Repositories;
using NumberGo.Models.Tables;

namespace NumberGo.Auth
{
    public class PremiumCheck : ActionFilterAttribute
    {
        UserRepository _userRepo;
        public PremiumCheck(UserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //登入檢查
            if (context.HttpContext.Session.TryGetValue("account", out byte[] bytes))
            {
                string userAccount = Encoding.UTF8.GetString(bytes);
                User user = _userRepo.GetUser(userAccount);
                //付費會員檢查
                if (!user.IsPremium)
                {
                    context.Result = new JsonResult(new { status = false, message = "Please upgrade account as premium member." });
                    return;
                }
            }
            else
            {
                context.Result = new JsonResult(new { status = false, message = "Please login first." });
                return;
            }
            base.OnActionExecuting(context);
        }
    }
}