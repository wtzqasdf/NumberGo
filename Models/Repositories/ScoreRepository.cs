using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NumberGo.Models.Contexts;
using NumberGo.Models.Tables;

namespace NumberGo.Models.Repositories
{
    public class ScoreRepository
    {
        ScoreContext _context;

        public ScoreRepository(ScoreContext context)
        {
            _context = context; 
        }

        /// <summary>
        /// 加入分數
        /// </summary>
        /// <returns>這會回傳查詢分數用的URL ID</returns>
        public string AddScore(string nickName, short level, TimeSpan elapsedTime)
        {
            Score score = new Score();
            string queryID = GenerateQueryID();
            score.QueryID = queryID;
            score.NickName = nickName;
            score.Level = level;
            score.PostDate = DateTime.Now;
            score.ElapsedTime = elapsedTime.ToString(@"hh\:mm\:ss\.fff");
            _context.Add(score);
            _context.SaveChanges();
            return queryID;
        }

        public Score GetScore(string queryID)
        {
            var scores = from s in _context.Score
                         where s.QueryID == queryID
                         select s;
            return scores.Count() > 0 ? scores.First() : null;
        }

        private string GenerateQueryID()
        {
            return Guid.NewGuid().ToString().Replace("-", string.Empty);
        }
    }
}
