using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KoiVeterinaryServiceCenter.DataAccess.Context;
using KoiVeterinaryServiceCenter.DataAccess.IRepository;
using KoiVeterinaryServiceCenter.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace KoiVeterinaryServiceCenter.DataAccess.Repository
{
    public class PoolRepository : Repository<Pool>, IPoolRepository
    {
        private readonly ApplicationDbContext _context;
        public PoolRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Pool> GetById(Guid poolId)
        {
            return await _context.Pool.Include(po => po.Customer).FirstOrDefaultAsync(po => po.PoolId == poolId);
        }

        public void Update(Pool pool)
        {
            _context.Update(pool);
        }
    }
}
