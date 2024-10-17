namespace KoiVeterinaryServiceCenter.DataAccess.IRepository;

public interface IUnitOfWork
{
    IDoctorRepository DoctorRepository { get; }
    ICustomerRepository CustomerRepository { get; }
    ISlotRepository SlotRepository { get; }
    IAppointmentRepository AppointmentRepository { get; }
    IDoctorSchedulesRepository DoctorSchedulesRepository { get; }
    IEmailTemplateRepository EmailTemplateRepository { get; }
    IServiceRepository ServiceRepository { get; }
    IDoctorServicesRepository DoctorServicesRepository { get; }
    Task<int> SaveAsync();
}