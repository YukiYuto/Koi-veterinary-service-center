using KoiVeterinaryServiceCenter.DataAccess.Context;
using KoiVeterinaryServiceCenter.DataAccess.IRepository;
using KoiVeterinaryServiceCenter.Model.Domain;
using Microsoft.EntityFrameworkCore;


namespace KoiVeterinaryServiceCenter.DataAccess.Repository
{
    public class AppointmentRepository : Repository<Appointment>, IAppointmentRepository
    {
        private readonly ApplicationDbContext _context;
        public AppointmentRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Appointment appointment)
        {
            _context.Appointments.Update(appointment);
        }
        public void UpdateRange(IEnumerable<Appointment> appointments)
        {
            _context.Appointments.UpdateRange(appointments);
        }

        public async Task<Appointment> GetAppointmentById(Guid appointmentId)
        {
            return await _context.Appointments.FirstOrDefaultAsync(x => x.AppointmentId == appointmentId);
        }
    }
}
