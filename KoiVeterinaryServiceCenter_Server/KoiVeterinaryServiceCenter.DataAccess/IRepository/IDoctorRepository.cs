using KoiVeterinaryServiceCenter.Model.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiVeterinaryServiceCenter.DataAccess.IRepository
{
    public interface IDoctorRepository : IRepository<Doctor>
    {
        void Update(Doctor doctor);
        void UpdateRange(IEnumerable<Doctor> doctors);
        Task<Doctor?> GetById(Guid id);
        Task<Doctor> AddAsync(Doctor doctor);
        Task<Doctor?> GetByUserId(string id);
    }
}
