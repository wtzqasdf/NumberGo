using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NumberGo.Models.Tables;

namespace NumberGo.Models.Contexts
{
    public class OrderContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }

        public OrderContext(DbContextOptions<OrderContext> opt) : base(opt)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Order>(e => {
                e.HasKey(k => k.OrderNo);
            });
        }
    }
}
