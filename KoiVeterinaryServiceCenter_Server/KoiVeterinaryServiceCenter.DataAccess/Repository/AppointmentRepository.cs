using KoiVeterinaryServiceCenter.DataAccess.Context;
using KoiVeterinaryServiceCenter.DataAccess.IRepository;
using KoiVeterinaryServiceCenter.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace KoiVeterinaryServiceCenter.DataAccess.Repository;

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

    public async Task<Appointment> GetAppointmentByAppmointNumer(long appointmentNumber)
    {
        return await _context.Appointments.FirstOrDefaultAsync(x => x.AppointmentNumber == appointmentNumber);
    }

    public async Task<long> GetMaxAppointmentNumberAsync()
    {
        // Truy vấn để lấy số AppointmentNumber lớn nhất hiện có
        return await _context.Appointments.MaxAsync(a => (long?)a.AppointmentNumber) ?? 0;
    }
    
    public async Task<IEnumerable<Appointment>> GetAppointmentsByUserId(string userId)
    {
        return await _context.Appointments
            .Where(a => a.CustomerId == userId)
            .ToListAsync();
    }
}