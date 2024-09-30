using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiVeterinaryServiceCenter.DataAccess.IRepository
{
    public interface IUnitOfWork
    {
        IDoctorRepository DoctorRepository { get; }
        ICustomerRepository CustomerRepository { get; }
        IRefreshTokenRepository RefreshTokens { get; }
        ISlotRepository SlotRepository { get; }
        IAppointmentRepository AppointmentRepository { get; }
        IEmailTemplateRepository EmailTemplateRepository { get; }
        Task<int> SaveAsync();
    }
}
