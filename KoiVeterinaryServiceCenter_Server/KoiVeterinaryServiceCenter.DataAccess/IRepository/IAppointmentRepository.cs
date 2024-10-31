using KoiVeterinaryServiceCenter.Models.Domain;
using KoiVeterinaryServiceCenter.Models.DTO.Appointment;

namespace KoiVeterinaryServiceCenter.DataAccess.IRepository;

public interface IAppointmentRepository : IRepository<Appointment>
{
    void Update(Appointment appointment);
    Task<Appointment> GetAppointmentById(Guid appointmentId);
    Task<Appointment> GetAppointmentByAppmointNumer(long appointmentNumber);
    Task<IEnumerable<Appointment>> GetAppointmentsByUserId(string userId);
    Task<long> GenerateUniqueNumberAsync();
    Task<List<GetAppointmentDTO>> GetAppointmentsWithDetails(string userId);


}