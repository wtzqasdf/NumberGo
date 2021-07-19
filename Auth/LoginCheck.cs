using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace NumberGo.Auth
{
    /// <summary>
    /// 驗證使用者是否已登入
    /// </summary>
    public class LoginCheck : ActionFilterAttribute
    {
        bool _isJson;
        public LoginCheck(bool isJson = true) 
        {
            _isJson = isJson;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.HttpContext.Session.Keys.Contains("account"))
            {
                if (_isJson)
                {
                    context.Result = new JsonResult(new { status = false, message = "Please login first." });
                }
                else
                {
                    context.Result = new RedirectToActionResult("Error403", "Error", null);
                }
                return;
            }
            base.OnActionExecuting(context);
        }
    }
}
