using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NumberGo.Models.Contexts;
using NumberGo.Models.Tables;

namespace NumberGo.Models.Repositories
{
    public class OrderRepository
    {
        OrderContext _context;
        public OrderRepository(OrderContext context)
        {
            _context = context;
        }

        public void AddOrder(string orderNo, string account, int price) 
        {
            Order order = new Order();
            order.OrderNo = orderNo;
            order.BuysafeNo = "";
            order.CreateDate = DateTime.Now;
            order.Account = account;
            order.Price = (ushort)price;
            order.CardNo4 = "";
            order.IsForeign = null;
            order.IsCompleted = false;
            _context.Add(order);
            _context.SaveChanges();
        }

        public void UpdateOrder(string orderNo, string buysafeNo, string cardNo4, bool isForeign, bool isCompleted)
        {
            Order order = (from o in _context.Orders
                          where o.OrderNo == orderNo
                          select o).First();
            order.BuysafeNo = buysafeNo;
            order.CardNo4 = cardNo4;
            order.IsForeign = isForeign;
            order.IsCompleted = isCompleted;
            _context.Update(order);
            _context.SaveChanges();
        }
    }
}
