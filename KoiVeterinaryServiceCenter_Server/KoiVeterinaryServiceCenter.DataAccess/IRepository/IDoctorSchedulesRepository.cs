using KoiVeterinaryServiceCenter.Models.Domain;

namespace KoiVeterinaryServiceCenter.DataAccess.IRepository
{
    public interface IDoctorSchedulesRepository : IRepository<DoctorSchedules>
    {
        void Update(DoctorSchedules doctorSchedules);
        void UpdateRange(IEnumerable<DoctorSchedules> doctorSchedules);
        Task<DoctorSchedules?> GetDocterScheduleById(Guid doctorScheduleId);
        Task<DoctorSchedules> GetDoctorScheduleById(Guid doctorSchedulesId);
    }
}