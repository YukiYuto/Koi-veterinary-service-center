using KoiVeterinaryServiceCenter.Models.Domain;

namespace KoiVeterinaryServiceCenter.DataAccess.IRepository;

public interface IAppointmentRepository : IRepository<Appointment>
{
    void Update(Appointment appointment);
    void UpdateRange(IEnumerable<Appointment> appointments);
    Task<Appointment> GetAppointmentById(Guid appointmentId);
    Task<Appointment> GetAppointmentByAppmointNumer(long appointmentNumber);
    Task<long> GetMaxAppointmentNumberAsync();
}