using KoiVeterinaryServiceCenter.DataAccess.Context;
using KoiVeterinaryServiceCenter.DataAccess.IRepository;
using KoiVeterinaryServiceCenter.Models.Domain;

namespace KoiVeterinaryServiceCenter.DataAccess.Repository;

public class AppointmentDepositRepository : Repository<AppointmentDeposit>, IAppointmentDepositRepository
{
    private readonly IUnitOfWork _unitOfWork;
    
    public AppointmentDepositRepository(ApplicationDbContext context, IUnitOfWork unitOfWork) : base(context)
    {
        _unitOfWork = unitOfWork;
    }

    public void Update(Slot slot)
    {
        throw new NotImplementedException();
    }

    public Task<AppointmentDeposit> GetAppointmentDepositByAppointmentId(Guid AppointmentId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<AppointmentDeposit>> GetAllSlotWithDoctor()
    {
        throw new NotImplementedException();
    }
}