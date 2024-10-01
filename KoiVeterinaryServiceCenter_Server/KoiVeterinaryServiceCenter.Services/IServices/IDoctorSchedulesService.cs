using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using KoiVeterinaryServiceCenter.Model.DTO;

namespace KoiVeterinaryServiceCenter.Services.IServices
{
    public interface IDoctorSchedulesService
    {
        Task<ResponseDTO> GetDoctorScheduleById(ClaimsPrincipal User, Guid doctorSchedulesId);
        Task<ResponseDTO> CreateDoctorSchedule(ClaimsPrincipal User, CreateDoctorSchedulesDTO createDoctorSchedulesDTO);
        Task<ResponseDTO> UpdateDoctorScheduleById(ClaimsPrincipal User, UpdateDoctorSchedulesDTO updateDoctorSchedulesDTO);
        Task<ResponseDTO> DeleteDoctorScheduleById(ClaimsPrincipal User, Guid doctorScheduleId);
    }
}
