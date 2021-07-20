using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NumberGo.Models.UserParams;
using NumberGo.Models.Contexts;
using NumberGo.Models.Tables;
using NumberGo.Models.Repositories;
using NumberGo.Utils;
using NumberGo.Auth;
using NumberGo.Extensions;

namespace NumberGo.Controllers
{
    public class UserController : BaseController
    {
        UserRepository _userRepo;
        ProfileRepository _profileRepo;
        MailSender _mailSender;
        public UserController(UserContext userContext, ProfileContext profileContext, MailSender mailSender)
        {
            _userRepo = new UserRepository(userContext);
            _profileRepo = new ProfileRepository(profileContext);
            _mailSender = mailSender;
        }

        [HttpPost]
        [LogoutCheck]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginData data)
        {
            if (!ModelState.IsValid)
            {
                return Json(false, errors: GetModelErrors());
            }
            if (!_userRepo.UserExists(data.Account) || !_userRepo.VerifyPassword(data.Account, data.Password))
            {
                string msg = "Account or password incorrect.";
                return Json(false, errors: new { account = msg, password = msg });
            }
            var user = _userRepo.GetUser(data.Account);
            SetSessionValue("account", user.Account);
            return Json(true);
        }

        [HttpPost]
        [LogoutCheck]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterData data)
        {
            if (!ModelState.IsValid)
            {
                return Json(false, errors: GetModelErrors());
            }
            if (data.Password != data.ConfirmPassword)
            {
                return Json(false, errors: new { confirmpassword = "Two password not same." });
            }
            if (_userRepo.UserExists(data.Account))
            {
                return Json(false, errors: new { account = "This account has exists." });
            }
            _userRepo.AddUser(data.Account, data.Password, data.Email);
            _profileRepo.AddProfile(data.Account);
            return Json(true, msg: "Register success.");
        }

        [HttpPost]
        [LoginCheck(isJson: true)]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            if (GetSessionValue("account") != null)
            {
                RemoveSessionValue("account");
            }
            return Json(true);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult GetProfile()
        {
            Dictionary<string, object> list = new Dictionary<string, object>();
            bool isLogin = GetSessionValue("account") != null;
            list.Add("haslogin", isLogin);
            if (isLogin)
            {
                //這裡登入之後才取得其他資料(例如遊戲點, Skins等等)
                Profile profile = _profileRepo.GetProfileOfAccount(GetSessionValue("account"));
                list.Add("account", profile.Account.HideHalfOfEnd());
                list.Add("point", profile.Point);
                //-------待加入付費會員判斷以及屬性物件
                list.Add("canupgrade", true); //<<<<<
            }
            return Json(list);
        }

        //未完成,2021-07-17
        [HttpPost]
        [LogoutCheck]
        [ValidateAntiForgeryToken]
        public IActionResult ForgotPW(ForgotPWData data)
        {
            if (!ModelState.IsValid)
            {
                return Json(false, errors: GetModelErrors());
            }

            return Json(null);
        }
    }
}
