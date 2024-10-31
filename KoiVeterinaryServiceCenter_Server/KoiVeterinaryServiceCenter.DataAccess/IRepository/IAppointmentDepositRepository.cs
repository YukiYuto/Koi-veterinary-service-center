using KoiVeterinaryServiceCenter.Models.Domain;

namespace KoiVeterinaryServiceCenter.DataAccess.IRepository;

public interface IAppointmentDepositRepository : IRepository<AppointmentDeposit>
{
    void Update(Slot slot);
    Task<AppointmentDeposit> GetAppointmentDepositByAppointmentId(Guid AppointmentId);
    Task<IEnumerable<AppointmentDeposit>> GetAllSlotWithDoctor();
}