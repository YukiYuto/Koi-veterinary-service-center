using KoiVeterinaryServiceCenter.Models.Domain;

namespace KoiVeterinaryServiceCenter.DataAccess.IRepository;

public interface ICustomerRepository : IRepository<ApplicationUser>
{
    void Update(ApplicationUser customer);
    void UpdateRange(IEnumerable<ApplicationUser> customers);
    Task<ApplicationUser?> GetById(string id);
    Task<ApplicationUser> AddAsync(ApplicationUser customer);
}