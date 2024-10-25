using KoiVeterinaryServiceCenter.Models.Domain;

namespace KoiVeterinaryServiceCenter.DataAccess.IRepository;

public interface IServiceRepository : IRepository<Service>
{
    void Update(Service service);
    void UpdateRange(IEnumerable<Service> services);
    Task<Service> GetServiceById(Guid serviceId);
    Task<Service> GetServiceByServiceNumber(long serviceId);
}