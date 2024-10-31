using KoiVeterinaryServiceCenter.DataAccess.Context;
using KoiVeterinaryServiceCenter.DataAccess.IRepository;
using KoiVeterinaryServiceCenter.Models.Domain;
using KoiVeterinaryServiceCenter.Models.DTO.Appointment;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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

    public async Task<List<GetAppointmentDTO>> GetAppointmentsWithDetails(string userId)
    {
        // Lấy danh sách các cuộc hẹn theo CustomerId  
        var appointments = await _context.Appointments
            .Where(a => a.CustomerId == userId) // Lọc theo CustomerId  
            .ToListAsync();

        // Lấy danh sách PetId từ các cuộc hẹn  
        var petIds = appointments.Select(a => a.PetId).Distinct().ToList();

        // Lấy thông tin chi tiết về Pet  
        var pets = await _context.Pets
            .Where(p => petIds.Contains(p.PetId))
            .ToListAsync();

        // Lấy thông tin chi tiết về Service  
        var serviceIds = appointments.Select(a => a.ServiceId).Distinct().ToList();
        var services = await _context.Services
            .Where(s => serviceIds.Contains(s.ServiceId))
            .ToListAsync();

        // Lấy thông tin chi tiết về Slot  
        var slotIds = appointments.Select(a => a.SlotId).Distinct().ToList();
        var slots = await _context.Slots
            .Where(s => slotIds.Contains(s.SlotId))
            .ToListAsync();

        // Lấy thông tin khách hàng  
        var customers = await _context.Users
            .Where(c => appointments.Select(a => a.CustomerId).Contains(c.Id))
            .ToListAsync();

        // Lấy thông tin bác sĩ từ bảng Doctor  
        var doctorIds = await _context.DoctorServices
            .Where(ds => serviceIds.Contains(ds.ServiceId))
            .Select(ds => ds.DoctorId)
            .Distinct()
            .ToListAsync();

        var doctors = await _context.Doctors
            .Include(d => d.ApplicationUser) // Bao gồm thông tin ApplicationUser để lấy tên bác sĩ  
            .Where(d => doctorIds.Contains(d.DoctorId))
            .ToListAsync();
        // Lấy thông tin dịch vụ và bác sĩ từ bảng DoctorServices  
        var doctorServices = await _context.DoctorServices
            .Include(ds => ds.Doctor.ApplicationUser) // Bao gồm thông tin ApplicationUser để lấy tên bác sĩ  
            .Include(ds => ds.Service) // Bao gồm thông tin Service  
            .Where(ds => serviceIds.Contains(ds.ServiceId))
            .ToListAsync();

        // Chuyển đổi danh sách Appointment thành danh sách GetAppointmentDTO  
        var appointmentDtos = appointments.Select(a => new GetAppointmentDTO
        {
            AppointmentId = a.AppointmentId,
            SlotId = a.SlotId,
            StartTime = slots.FirstOrDefault(s => s.SlotId == a.SlotId)?.StartTime,
            EndTime = slots.FirstOrDefault(s => s.SlotId == a.SlotId)?.EndTime,
            ServiceId = a.ServiceId,
            ServiceName = services.FirstOrDefault(s => s.ServiceId == a.ServiceId)?.ServiceName,
            PetId = a.PetId,
            PetName = pets.FirstOrDefault(p => p.PetId == a.PetId)?.Name,
            BookingStatus = a.BookingStatus,
            BookingStatusDescription = a.BookingStatusDescription,
            TotalAmount = a.TotalAmount,
            AppointmentDate = a.AppointmentDate,
            Description = a.Description,
            CustomerId = a.CustomerId,
            CustomerName = customers.FirstOrDefault(c => c.Id == a.CustomerId)?.FullName,
            AppointmentNumber = a.AppointmentNumber,
            DoctorId = doctorServices.FirstOrDefault(ds => ds.ServiceId == a.ServiceId)?.DoctorId ?? Guid.Empty, // Lấy DoctorId  
            DoctorName = doctorServices.FirstOrDefault(ds => ds.ServiceId == a.ServiceId)?.Doctor.ApplicationUser.FullName // Lấy tên bác sĩ từ ApplicationUser 
        }).ToList();

        return appointmentDtos;
    }
}