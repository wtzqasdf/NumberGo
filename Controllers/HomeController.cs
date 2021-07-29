using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NumberGo.Models.Repositories;
using NumberGo.Models.Tables;
using NumberGo.Models.Contexts;
using NumberGo.Auth;
using NumberGo.Utils;

namespace NumberGo.Controllers
{
    public class HomeController : Controller
    {
        const string FILEVERSION = "?1.04";
        private readonly ILogger<HomeController> _logger;
        ScoreRepository _scoreRepo;

        public HomeController(ILogger<HomeController> logger, ScoreContext context)
        {
            _logger = logger;
            _scoreRepo = new ScoreRepository(context);
        }

        public IActionResult Index()
        {
            ViewData["Title"] = "NumberGo";
            ViewData["Desc"] = "Number and calculation game.";
            ViewData["Url"] = string.Format("https://{0}", Request.Host.Value);
            ViewData["FileVer"] = FILEVERSION;
            return View();
        }

        /// <summary>
        /// 用於分享分數的短網址
        /// </summary>
        /// <returns></returns>
        public IActionResult S(string id)
        {
            if (HttpHelper.IsCrawler(Request.Headers["User-Agent"].ToString()))
            {
                Score score = _scoreRepo.GetScore(id);
                if (score != null)
                {
                    ViewData["Title"] = $"{score.NickName} in level{score.Level} cost {score.ElapsedTime} - NumberGo";
                    ViewData["Desc"] = $"{score.NickName} in level{score.Level} cost {score.ElapsedTime}";
                    ViewData["Url"] = string.Format("https://{0}/s/{1}", Request.Host.Value, score.QueryID);
                    ViewData["Image"] = string.Format("https://{0}/img/logo.jpg", Request.Host.Value);
                    ViewData["FileVer"] = FILEVERSION;
                    return View();
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return Redirect("/");
            }
        }

    }
}
