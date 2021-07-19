using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NumberGo.Models.Contexts;
using NumberGo.Models.Tables;

namespace NumberGo.Models.Repositories
{
    public class ProfileRepository
    {
        ProfileContext _context;

        public ProfileRepository(ProfileContext context)
        {
            _context = context;
        }

        public void AddProfile(string account)
        {
            var profile = new Profile()
            {
                Account = account,
                Point = 0
            };
            _context.Add(profile);
            _context.SaveChanges();
        }

        public bool ProfileExists(string account)
        {
            var profile = from p in _context.Profile
                          where p.Account == account
                          select p;
            return profile.Count() > 0;
        }

        public Profile GetProfileOfAccount(string account) 
        {
            var profile = from p in _context.Profile
                          where p.Account == account
                          select p;
            return profile.First();
        }
    }
}
