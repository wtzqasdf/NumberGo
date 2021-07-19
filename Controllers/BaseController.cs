using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace NumberGo.Controllers
{
    public class BaseController : Controller
    {

        protected void SetSessionValue(string key, string value)
        {
            HttpContext.Session.Set(key, Encoding.UTF8.GetBytes(value));
        }

        protected string GetSessionValue(string key)
        {
            if (HttpContext.Session.TryGetValue(key, out byte[] bytes))
            {
                return Encoding.UTF8.GetString(bytes);
            }
            return null;
        }

        protected void RemoveSessionValue(string key)
        {
            HttpContext.Session.Remove(key);
        }

        protected JsonResult Json(bool stat, object errors = null, string msg = null)
        {
            if (errors != null)
            {
                return base.Json(new { status = stat, errormsgs = errors });
            }
            if (msg != null)
            {
                return base.Json(new { status = stat, message = msg });
            }
            return base.Json(new { status = stat });
        }

        protected Dictionary<string, string> GetModelErrors()
        {
            Dictionary<string, string> msgs = new Dictionary<string, string>();
            foreach (var key in ModelState.Keys)
            {
                string msg = "";
                foreach (var error in ModelState[key].Errors)
                {
                    msg += error.ErrorMessage;
                }
                msgs.Add(key.ToLower(), msg);
            }
            return msgs;
        }
    }
}
