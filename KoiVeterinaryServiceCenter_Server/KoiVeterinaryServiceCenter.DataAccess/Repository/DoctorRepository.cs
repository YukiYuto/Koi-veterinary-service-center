using KoiVeterinaryServiceCenter.DataAccess.Context;
using KoiVeterinaryServiceCenter.DataAccess.IRepository;
using KoiVeterinaryServiceCenter.Model.Domain;
using Microsoft.EntityFrameworkCore;


namespace KoiVeterinaryServiceCenter.DataAccess.Repository
{
    public class DoctorRepository : Repository<Doctor>, IDoctorRepository
    {
        private readonly ApplicationDbContext _context;

        public DoctorRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Doctor?> GetById(Guid id)
        {
            return await _context.Doctors.Include("ApplicationUser").FirstOrDefaultAsync(x => x.DoctorId == id);
        }

        public async Task<Doctor?> GetByUserId(string id)
        {
            return await _context.Doctors.Include("ApplicationUser").FirstOrDefaultAsync(x => x.UserId == id);
        }

        public void Update(Doctor doctor)
        {
            _context.Doctors.Update(doctor);
        }

        public void UpdateRange(IEnumerable<Doctor> doctors)
        {
            _context.Doctors.UpdateRange(doctors);
        }

        public async Task<Doctor> AddAsync(Doctor doctor)
        {
            var enityEntry = await _context.Doctors.AddAsync(doctor);
            return enityEntry.Entity;
        }
    }
}
