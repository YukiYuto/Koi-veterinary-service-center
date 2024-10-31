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

    public async Task<Appointment> GetAppointmentById(Guid appointmentId)
    {
        return await _context.Appointments.FirstOrDefaultAsync(x => x.AppointmentId == appointmentId);
    }

    public async Task<Appointment> GetAppointmentByAppmointNumer(long appointmentNumber)
    {
        return await _context.Appointments.FirstOrDefaultAsync(x => x.AppointmentNumber == appointmentNumber);
    }
    public async Task<IEnumerable<Appointment>> GetAppointmentsByUserId(string userId)
    {
        return await _context.Appointments
            .Where(a => a.CustomerId == userId)
            .ToListAsync();
    }

    public async Task<long> GenerateUniqueNumberAsync()
    {
        long nextNumber = 1;
        while (true)
        {
            // Kiểm tra xem số hiệu đã tồn tại trong bảng Appointment hay AppointmentDeposit chưa
            if (!(await _context.Appointments.AnyAsync(a => a.AppointmentNumber == nextNumber)) &&
                !(await _context.AppointmentDeposits.AnyAsync(ad => ad.AppointmentDepositNumber == nextNumber)))
            {
                return nextNumber;
            }
            nextNumber++;
        }
    }
}