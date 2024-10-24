using KoiVeterinaryServiceCenter.DataAccess.Context;
using KoiVeterinaryServiceCenter.DataAccess.IRepository;
using KoiVeterinaryServiceCenter.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace KoiVeterinaryServiceCenter.DataAccess.Repository;

public class ServiceRepository : Repository<Service>, IServiceRepository
{
    public readonly ApplicationDbContext _context;
    public ServiceRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Service> GetServiceById(Guid serviceId)
    {
        return await _context.Services.FirstOrDefaultAsync(x => x.ServiceId == serviceId);
    }

    public void Update(Service service)
    {
        _context.Services.Update(service);
    }

    public void UpdateRange(IEnumerable<Service> services)
    {
        _context.Services.UpdateRange(services);
    }
}