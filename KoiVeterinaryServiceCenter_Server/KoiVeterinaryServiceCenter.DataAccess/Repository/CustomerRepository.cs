using KoiVeterinaryServiceCenter.DataAccess.Context;
using KoiVeterinaryServiceCenter.DataAccess.IRepository;
using KoiVeterinaryServiceCenter.Model.Domain;
using Microsoft.EntityFrameworkCore;

namespace KoiVeterinaryServiceCenter.DataAccess.Repository
{

    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        private readonly ApplicationDbContext _context;

        public CustomerRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Customer?> GetById(Guid id)
        {
            return await _context.Customers.Include("ApplicationUser").FirstOrDefaultAsync(x => x.CustomerId == id);
        }

        public async Task<Customer?> GetByUserId(string id)
        {
            return await _context.Customers.Include("ApplicationUser").FirstOrDefaultAsync(x => x.UserId == id);
        }

        public void Update(Customer customer)
        {
            _context.Customers.Update(customer);
        }
        
        public void UpdateRange(IEnumerable<Customer> customers)
        {
            _context.Customers.UpdateRange(customers);
        }

        public async Task<Customer> AddAsync(Customer customer)
        {
            var enityEntry = await _context.Customers.AddAsync(customer);
            return enityEntry.Entity;
        }
    }
}