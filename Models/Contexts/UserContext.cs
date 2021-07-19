using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NumberGo.Models.Tables;

namespace NumberGo.Models.Contexts
{
    public class UserContext : DbContext
    {
        public DbSet<User> User { get; set; }

        public UserContext(DbContextOptions<UserContext> opt) : base(opt)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>(e => {
                e.HasKey(k => k.Account);
            });
        }
    }
}
