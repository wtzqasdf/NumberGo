using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NumberGo.Models.Tables;

namespace NumberGo.Models.Contexts
{
    public class ScoreContext : DbContext
    {
        public DbSet<Score> Score { get; set; }

        public ScoreContext(DbContextOptions<ScoreContext> opt) : base(opt)
        { 
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
 
            builder.Entity<Score>(e => {
                e.HasKey(k => k.QueryID);
            });
        }
    }
}
