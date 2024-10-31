using KoiVeterinaryServiceCenter.Models.Domain;

namespace KoiVeterinaryServiceCenter.DataAccess.IRepository;

public interface IAppointmentDepositRepository : IRepository<AppointmentDeposit>
{
    void Update(AppointmentDeposit appointmentDeposit);
    Task<AppointmentDeposit> GetAppointmentDepositByAppointmentId(Guid AppointmentId);
    Task<long> GenerateUniqueNumberAsync();
}