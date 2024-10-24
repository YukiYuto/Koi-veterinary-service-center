using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KoiVeterinaryServiceCenter.Model.Domain;

namespace KoiVeterinaryServiceCenter.DataAccess.IRepository
{
    public interface IDoctorServicesRepository : IRepository<DoctorServices>
    {
        Task<DoctorServices> GetById(Guid doctorServiceId);
        void Update (DoctorServices doctorService);
        void UpdateRange (DoctorServices doctorService);


        Task<DoctorServices> GetDoctorServiceByDoctorAndServiceId(Guid doctorId, Guid serviceId);
        Task<List<DoctorServices>> GetByServiceId(Guid serviceId);
    }
}
