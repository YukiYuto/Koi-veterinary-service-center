using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KoiVeterinaryServiceCenter.Models.Domain;

namespace KoiVeterinaryServiceCenter.DataAccess.IRepository
{
    public interface IPoolRepository : IRepository<Pool>
    {
        void Update(Pool pool);
        Task<Pool> GetById(Guid poolId);
    }
}
