using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KoiVeterinaryServiceCenter.DataAccess.Context;
using KoiVeterinaryServiceCenter.DataAccess.IRepository;
using KoiVeterinaryServiceCenter.Model.Domain;
using Microsoft.EntityFrameworkCore;

namespace KoiVeterinaryServiceCenter.DataAccess.Repository
{
    public class OrderItemsRepository : Repository<OrderItems>, IOrderItemsRepository
    {
        private readonly ApplicationDbContext _context;
        public OrderItemsRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<OrderItems> GetById(int id)
        {
            return await _context.OrderItems.FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Update(OrderItems orderItems)
        {
            _context.OrderItems.Update(orderItems);
        }
    }
}
