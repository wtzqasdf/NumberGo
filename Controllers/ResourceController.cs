using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace NumberGo.Controllers
{
    public class ResourceController : Controller
    {
        //加入CSRF功能
        public IActionResult Index()
        {
            //這邊只要傳回音效部分
            return View();
        }
    }
}
