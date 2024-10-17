using KoiVeterinaryServiceCenter.Models.Domain;

namespace KoiVeterinaryServiceCenter.DataAccess.IRepository;

public interface IDoctorServicesRepository : IRepository<DoctorServices>
{
    Task<DoctorServices> GetById(Guid doctorServiceId);
    void Update (DoctorServices doctorService);
    void UpdateRange (DoctorServices doctorService);
}