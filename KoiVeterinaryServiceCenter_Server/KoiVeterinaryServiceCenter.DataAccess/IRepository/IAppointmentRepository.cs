using KoiVeterinaryServiceCenter.Models.Domain;

namespace KoiVeterinaryServiceCenter.DataAccess.IRepository;

public interface IAppointmentRepository : IRepository<Appointment>
{
    void Update(Appointment appointment);
    Task<Appointment> GetAppointmentById(Guid appointmentId);
    Task<Appointment> GetAppointmentByAppmointNumer(long appointmentNumber);
    Task<IEnumerable<Appointment>> GetAppointmentsByUserId(string userId);
    Task<long> GenerateUniqueNumberAsync();
    
}