using KoiVeterinaryServiceCenter.DataAccess.Context;
using KoiVeterinaryServiceCenter.DataAccess.IRepository;
using KoiVeterinaryServiceCenter.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace KoiVeterinaryServiceCenter.DataAccess.Repository;

public class DoctorServicesRepository : Repository<DoctorServices>, IDoctorServicesRepository
{
    public readonly ApplicationDbContext _context;
    public DoctorServicesRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<DoctorServices> GetById(Guid doctorServiceId)
    {
        return await _context.DoctorServices.Include(ds => ds.Doctor).ThenInclude(d => d.ApplicationUser).Include(ds => ds.Service).FirstOrDefaultAsync(x => x.ServiceId == doctorServiceId);
    }

    public void Update(DoctorServices doctorService)
    {
        _context.DoctorServices.Update(doctorService);
    }

    public void UpdateRange(DoctorServices doctorService)
    {
        _context.DoctorServices.UpdateRange(doctorService);
    }
}