using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NumberGo.Models.Tables;

namespace NumberGo.Models.Contexts
{
    public class ProfileContext : DbContext
    {
        public DbSet<Profile> Profile { get; set; }

        public ProfileContext(DbContextOptions<ProfileContext> opt) : base(opt)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Profile>(e => {
                e.HasKey(k => k.Account);
            });
        }
    }
}
