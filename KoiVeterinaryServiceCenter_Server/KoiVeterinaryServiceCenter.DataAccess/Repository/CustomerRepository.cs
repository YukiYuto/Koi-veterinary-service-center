using KoiVeterinaryServiceCenter.DataAccess.Context;
using KoiVeterinaryServiceCenter.DataAccess.IRepository;
using KoiVeterinaryServiceCenter.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace KoiVeterinaryServiceCenter.DataAccess.Repository;

public class CustomerRepository : Repository<ApplicationUser>, ICustomerRepository
{
    private readonly ApplicationDbContext _context;

    public CustomerRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<ApplicationUser?> GetById(string id)
    {
        return await _context.ApplicationUsers.FirstOrDefaultAsync(x => x.Id == id);
    }

    public void Update(ApplicationUser customer)
    {
        _context.ApplicationUsers.Update(customer);
    }

    public void UpdateRange(IEnumerable<ApplicationUser> customers)
    {
        _context.ApplicationUsers.UpdateRange(customers);
    }

    public async Task<ApplicationUser> AddAsync(ApplicationUser customer)
    {
        var enityEntry = await _context.ApplicationUsers.AddAsync(customer);
        return enityEntry.Entity;
    }
}