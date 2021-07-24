using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NumberGo.Auth;
using NumberGo.Models.Contexts;
using NumberGo.Models.Tables;
using NumberGo.Models.Repositories;
using NumberGo.Utils;

namespace NumberGo.Controllers
{
    public class SoundController : BaseController
    {
        UserRepository _userRepo;
        string _soundDir;
        public SoundController(UserContext context)
        {
            _userRepo = new UserRepository(context);
            _soundDir = string.Format("{0}/sounds/", Directory.GetCurrentDirectory());
        }

        [HttpGet]
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = int.MaxValue)]
        public async Task<IActionResult> Scale()
        {
            byte[] bytes = await FileHelper.ReadToBytes(_soundDir + "scale.mp3");
            return File(bytes, "audio/mpeg");
        }

        [HttpGet]
        [LoginCheck]
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = int.MaxValue)]
        public async Task<IActionResult> Clock()
        {
            var user = _userRepo.GetUser(GetSessionValue("account"));
            if (!user.IsPremium)
            {
                return Json(false, msg: "account not premium.");
            }
            byte[] bytes = await FileHelper.ReadToBytes(_soundDir + "clock.mp3");
            return File(bytes, "audio/mpeg");
        }

        [HttpGet]
        [LoginCheck]
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = int.MaxValue)]
        public async Task<IActionResult> Ghost()
        {
            var user = _userRepo.GetUser(GetSessionValue("account"));
            if (!user.IsPremium)
            {
                return Json(false, msg: "account not premium.");
            }
            byte[] bytes = await FileHelper.ReadToBytes(_soundDir + "ghost.mp3");
            return File(bytes, "audio/mpeg");
        }

        [HttpGet]
        [LoginCheck]
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = int.MaxValue)]
        public async Task<IActionResult> Gear()
        {
            var user = _userRepo.GetUser(GetSessionValue("account"));
            if (!user.IsPremium)
            {
                return Json(false, msg: "account not premium.");
            }
            byte[] bytes = await FileHelper.ReadToBytes(_soundDir + "gear.mp3");
            return File(bytes, "audio/mpeg");
        }
    }
}
