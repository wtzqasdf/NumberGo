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
        MailSender _mailSender;
        public UserController(UserContext userContext, MailSender mailSender)
        {
            _userRepo = new UserRepository(userContext);
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
            //登入之後才取得其他資料
            if (isLogin)
            {
                User user = _userRepo.GetUser(GetSessionValue("account"));
                list.Add("account", user.Account.HideHalfOfEnd());
                list.Add("ispremium", user.IsPremium);
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
