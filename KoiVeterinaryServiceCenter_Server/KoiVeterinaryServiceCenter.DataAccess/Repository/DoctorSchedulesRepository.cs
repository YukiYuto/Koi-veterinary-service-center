using KoiVeterinaryServiceCenter.DataAccess.Context;
using KoiVeterinaryServiceCenter.DataAccess.IRepository;
using KoiVeterinaryServiceCenter.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace KoiVeterinaryServiceCenter.DataAccess.Repository
{
    public class DoctorSchedulesRepository : Repository<DoctorSchedules>, IDoctorSchedulesRepository
    {
        private readonly ApplicationDbContext _context;
        public DoctorSchedulesRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<DoctorSchedules> GetDocterScheduleById(Guid doctorScheduleId)
        {
            return await _context.DoctorSchedules.FirstOrDefaultAsync(x => x.DoctorSchedulesId == doctorScheduleId);
        }

        public void Update(DoctorSchedules doctorSchedules)
        {
            _context.DoctorSchedules.Update(doctorSchedules);
        }

        public void UpdateRange(IEnumerable<DoctorSchedules> doctorSchedules)
        {
            _context.DoctorSchedules.UpdateRange(doctorSchedules);
        }

        public async Task<DoctorSchedules> GetDoctorScheduleById(Guid doctorSchedulesId)
        {
            return await _context.DoctorSchedules
                .FirstOrDefaultAsync(ds => ds.DoctorSchedulesId == doctorSchedulesId);
        }
    }
}