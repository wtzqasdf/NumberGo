using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NumberGo.Models.Contexts;
using NumberGo.Models.Tables;

namespace NumberGo.Models.Repositories
{
    public class UserRepository
    {
        UserContext _context;
        //作為緩存使用
        User _user;

        public UserRepository(UserContext context)
        {
            _context = context;
            _user = null;
        }

        public void AddUser(string account, string password, string email)
        {
            var user = new User()
            {
                Account = account,
                Password = BCrypt.Net.BCrypt.HashPassword(password),
                Email = email,
                RegDate = DateTime.Now,
                IsPremium = false
            };
            _context.Add(user);
            _context.SaveChanges();
        }

        public bool UserExists(string account)
        {
            var list = from user in _context.User
                       where user.Account == account
                       select user;
            if (list.Count() == 0)
            {
                return false;
            }
            _user = list.First();
            return true;
        }

        public bool VerifyPassword(string account, string password)
        {
            if (_user == null || _user.Account != account)
            {
                var list = from user in _context.User
                           where user.Account == account
                           select user;
                _user = list.First();
            }
            return BCrypt.Net.BCrypt.Verify(password, _user.Password);
        }

        public void OverWritePassword(string account, string password)
        {
            if (_user == null || _user.Account != account)
            {
                var list = from user in _context.User
                           where user.Account == account
                           select user;
                _user = list.First();
            }
            _user.Password = BCrypt.Net.BCrypt.HashPassword(password);
            _context.Update(_user);
            _context.SaveChanges();
        }
        public User GetUser(string account)
        {
            if (_user != null && _user.Account == account)
            {
                return _user;
            }
            var list = from user in _context.User
                       where user.Account == account
                       select user;
            return list.First();
        }

        public void UpdatePremium(string account)
        {
            User user = (from u in _context.User
                         where u.Account == account
                         select u).First();
            user.IsPremium = true;
            _context.Update(user);
            _context.SaveChanges();
        }
    }
}
