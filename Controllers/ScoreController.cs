using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NumberGo.Models.UserParams;
using NumberGo.Models.Contexts;
using NumberGo.Models.Repositories;

namespace NumberGo.Controllers
{
    public class ScoreController : BaseController
    {
        ScoreRepository _scoreRepo;
        public ScoreController(ScoreContext context)
        {
            _scoreRepo = new ScoreRepository(context);
        }

        [ValidateAntiForgeryToken]
        public IActionResult Record(ScoreData data)
        {
            if (!ModelState.IsValid)
            {
                return Json(false, errors: GetModelErrors());
            }
            string queryID = _scoreRepo.AddScore(data.NickName, data.Level, data.ElapsedTime);
            string fullUrl = string.Format("https://{0}/s/{1}", Request.Host.Value, queryID);
            return Json(new { url = fullUrl });
        }
    }
}
