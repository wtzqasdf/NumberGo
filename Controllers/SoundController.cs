using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NumberGo.Auth;
using NumberGo.Models.Contexts;
using NumberGo.Models.Tables;
using NumberGo.Models.Repositories;

namespace NumberGo.Controllers
{
    public class SoundController : Controller
    {
        UserRepository _userRepo;
        public SoundController(UserContext context)
        {
            _userRepo = new UserRepository(context);
        }

        [HttpGet]
        public IActionResult Scale()
        {
            //提供音效資源
            return null;
        }

        [HttpGet]
        public IActionResult Clock()
        {
            //提供音效資源
            return null;
        }

        [HttpGet]
        public IActionResult Ghost()
        {
            //提供音效資源
            return null;
        }

        [HttpGet]
        public IActionResult Gear()
        {
            //提供音效資源
            return null;
        }
    }
}
