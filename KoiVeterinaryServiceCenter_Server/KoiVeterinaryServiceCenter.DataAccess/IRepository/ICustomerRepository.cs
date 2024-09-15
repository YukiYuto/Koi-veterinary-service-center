using KoiVeterinaryServiceCenter.Model.Domain;

namespace KoiVeterinaryServiceCenter.DataAccess.IRepository;

public interface ICustomerRepository : IRepository<Customer>
{
    void Update(Customer customer);
    void UpdateRange(IEnumerable<Customer> customers);
    Task<Customer?> GetById(Guid id);
    Task<Customer> AddAsync(Customer customer);
    Task<Customer?> GetByUserId(string id);
}