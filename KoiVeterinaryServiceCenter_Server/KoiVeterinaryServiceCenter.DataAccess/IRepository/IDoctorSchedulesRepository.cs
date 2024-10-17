using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KoiVeterinaryServiceCenter.Model.Domain;

namespace KoiVeterinaryServiceCenter.DataAccess.IRepository
{
    public interface IDoctorSchedulesRepository : IRepository<DoctorSchedules>
    {
        void Update(DoctorSchedules doctorSchedules);
        void UpdateRange(IEnumerable<DoctorSchedules> doctorSchedules);
        Task<DoctorSchedules?> GetDocterScheduleById(Guid doctorScheduleId);
    }
}
