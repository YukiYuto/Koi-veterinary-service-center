using KoiVeterinaryServiceCenter.DataAccess.Context;
using KoiVeterinaryServiceCenter.DataAccess.IRepository;
using KoiVeterinaryServiceCenter.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace KoiVeterinaryServiceCenter.DataAccess.Repository;

public class AppointmentDepositRepository : Repository<AppointmentDeposit>, IAppointmentDepositRepository
{
    private readonly ApplicationDbContext _context;

    public AppointmentDepositRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public void Update(AppointmentDeposit appointmentDeposit)
    {
        _context.AppointmentDeposits.Update(appointmentDeposit);
    }

    public Task<AppointmentDeposit> GetAppointmentDepositByAppointmentId(Guid AppointmentId)
    {
        throw new NotImplementedException();
    }


    public async Task<long> GenerateUniqueNumberAsync()
    {
        long nextNumber = 1;
        while (true)
        {
            // Kiểm tra xem số hiệu đã tồn tại trong bảng AppointmentDeposit chưa
            var existsInDeposits = await _context.AppointmentDeposits.AnyAsync(ad => ad.AppointmentDepositNumber == nextNumber);
            if (!existsInDeposits)
            {
                return nextNumber;
            }

            nextNumber++;
        }
    }
}