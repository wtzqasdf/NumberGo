using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace NumberGo.Auth
{
    /// <summary>
    /// 驗證使用者是否已登出
    /// </summary>
    public class LogoutCheck : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Session.Keys.Contains("account"))
            {
                context.Result = new JsonResult(new { status = false, message = "Please logout first." });
                return;
            }
            base.OnActionExecuting(context);
        }
    }
}
