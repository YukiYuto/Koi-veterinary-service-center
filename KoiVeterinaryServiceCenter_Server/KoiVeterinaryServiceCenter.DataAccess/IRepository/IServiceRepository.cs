using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KoiVeterinaryServiceCenter.Model.Domain;

namespace KoiVeterinaryServiceCenter.DataAccess.IRepository
{
    public interface IServiceRepository : IRepository<Service>
    {
        void Update(Service service);
        void UpdateRange(IEnumerable<Service> services);
        Task<Service> GetServiceById(Guid serviceId);
        Task<Service> GetServiceByServiceNumber(long serviceId);
    }
}
