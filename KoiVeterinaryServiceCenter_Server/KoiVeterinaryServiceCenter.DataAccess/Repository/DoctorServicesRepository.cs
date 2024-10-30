using KoiVeterinaryServiceCenter.DataAccess.Context;
using KoiVeterinaryServiceCenter.DataAccess.IRepository;
using KoiVeterinaryServiceCenter.Model.Domain;
using Microsoft.EntityFrameworkCore;

namespace KoiVeterinaryServiceCenter.DataAccess.Repository
{
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

        public async Task<List<DoctorServices>> GetByServiceId(Guid serviceId)
        {
      
                return await _context.DoctorServices.Include(ds => ds.Doctor).ThenInclude(d => d.ApplicationUser).Include(ds => ds.Service).Where(ds => ds.ServiceId == serviceId) // Filter by ServiceId
.ToListAsync(); // Execute the query and return the results as a List
            }

            public void Update(DoctorServices doctorService)
        {
            _context.DoctorServices.Update(doctorService);
        }
        public async Task<DoctorServices> GetDoctorServiceByDoctorAndServiceId(Guid doctorId, Guid serviceId)
        {
            return await _context.DoctorServices
                .Include(ds => ds.Doctor)
                .ThenInclude(d => d.ApplicationUser)
                .Include(ds => ds.Service)
                .FirstOrDefaultAsync(ds => ds.DoctorId == doctorId && ds.ServiceId == serviceId);
        }


        public void UpdateRange(DoctorServices doctorService)
        {
            _context.DoctorServices.UpdateRange(doctorService);
        }
    }
}
