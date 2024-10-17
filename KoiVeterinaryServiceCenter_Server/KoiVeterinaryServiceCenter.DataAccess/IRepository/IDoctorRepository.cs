using KoiVeterinaryServiceCenter.Models.Domain;

namespace KoiVeterinaryServiceCenter.DataAccess.IRepository;

public interface IDoctorRepository : IRepository<Doctor>
{
    void Update(Doctor doctor);
    void UpdateRange(IEnumerable<Doctor> doctors);
    Task<Doctor?> GetById(Guid id);
    Task<Doctor> AddAsync(Doctor doctor);
    Task<Doctor?> GetByUserId(string id);
}