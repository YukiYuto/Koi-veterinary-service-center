using System.Security.Claims;
using KoiVeterinaryServiceCenter.Model.DTO;
using KoiVeterinaryServiceCenter.Models.DTO;
using KoiVeterinaryServiceCenter.Models.DTO.DoctorSchedules;

namespace KoiVeterinaryServiceCenter.Services.IServices
{
    public interface IDoctorSchedulesService
    {
        Task<ResponseDTO> GetAll(
                ClaimsPrincipal User,
                string? filterOn,
                string? filterQuery,
                string? sortBy,
                bool? isAscending,
                int pageNumber,
                int pageSize
            );
        Task<ResponseDTO> GetDoctorScheduleById(ClaimsPrincipal User, Guid doctorSchedulesId);
        Task<ResponseDTO> GetDoctorScheduleByDoctorId(ClaimsPrincipal User, Guid doctorId);
        Task<ResponseDTO> CreateDoctorSchedule(ClaimsPrincipal User, CreateDoctorSchedulesDTO createDoctorSchedulesDTO);
        Task<ResponseDTO> UpdateDoctorScheduleById(ClaimsPrincipal User, UpdateDoctorSchedulesDTO updateDoctorSchedulesDTO);
        Task<ResponseDTO> DeleteDoctorScheduleById(ClaimsPrincipal User, Guid doctorScheduleId);
    }
}
