using KoiVeterinaryServiceCenter.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace KoiVeterinaryServiceCenter.Services.IServices
{
    public interface IAppointmentService
    {
        Task<ResponseDTO> GetAppointments
    (
        ClaimsPrincipal User,
        string? filterOn,
        string? filterQuery,
        string? sortBy,
        bool? isAscending,
        int pageNumber = 0,
        int pageSize = 0
    );

        Task<ResponseDTO> GetAppointment(ClaimsPrincipal User, Guid appointmentId);
        Task<ResponseDTO> CreateAppointment(ClaimsPrincipal User, CreateAppointmentDTO createAppointmentDto);
        Task<ResponseDTO> UpdateAppointment(ClaimsPrincipal User, UpdateAppointmentDTO updateAppointmentDto);
        Task<ResponseDTO> DeleteAppointment(ClaimsPrincipal User, Guid appointmentId);
    }
}
