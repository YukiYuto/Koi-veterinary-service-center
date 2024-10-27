using KoiVeterinaryServiceCenter.DataAccess.Context;
using KoiVeterinaryServiceCenter.DataAccess.IRepository;
using KoiVeterinaryServiceCenter.Models.Domain;
using Microsoft.AspNetCore.Identity;

namespace KoiVeterinaryServiceCenter.DataAccess.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    public IDoctorRepository DoctorRepository { get; set; }
    public ICustomerRepository CustomerRepository { get; set; }
    public ISlotRepository SlotRepository { get; set; }
    public IAppointmentRepository AppointmentRepository { get; set; }
    public IEmailTemplateRepository EmailTemplateRepository { get; set; }
    public IDoctorSchedulesRepository DoctorSchedulesRepository { get; set; }
    public IServiceRepository ServiceRepository { get; set; }
    public IDoctorServicesRepository DoctorServicesRepository { get; set; }

    public IDoctorRatingRepository DoctorRatingRepository { get; set; }
    public IPaymentTransactionsRepository PaymentTransactionsRepository { get; set; }
    public UnitOfWork(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        DoctorRepository = new DoctorRepository(_context);
        CustomerRepository = new CustomerRepository(_context);
        SlotRepository = new SlotRepository(_context);
        AppointmentRepository = new AppointmentRepository(_context);
        EmailTemplateRepository = new EmailTemplateRepository(_context);
        DoctorSchedulesRepository = new DoctorSchedulesRepository(_context);
        DoctorRatingRepository = new DoctorRatingRepository(_context);
        ServiceRepository = new ServiceRepository(_context);
        DoctorServicesRepository = new DoctorServicesRepository(_context);
        PaymentTransactionsRepository = new PaymentTransactionsRepository(_context);
    }
    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }
}