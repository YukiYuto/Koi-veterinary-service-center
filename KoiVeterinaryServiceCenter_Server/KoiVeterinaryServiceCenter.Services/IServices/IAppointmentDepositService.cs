using System.Security.Claims;
using KoiVeterinaryServiceCenter.Models.DTO;
using KoiVeterinaryServiceCenter.Models.DTO.Appointment;
using KoiVeterinaryServiceCenter.Models.DTO.AppointmentPet;

namespace KoiVeterinaryServiceCenter.Services.IServices;

public interface IAppointmentDepositService
{
    Task<ResponseDTO> GetAppointmentDeposits
    (
        ClaimsPrincipal User,
        string? filterOn,
        string? filterQuery,
        string? sortBy,
        int pageNumber = 0,
        int pageSize = 0
    );

    Task<ResponseDTO> GetAppointmentDepositById(ClaimsPrincipal User, Guid appointmentId);
    Task<ResponseDTO> CreateAppointmentDeposit(ClaimsPrincipal User, CreateAppointmentDepositDTO createAppointmentDepositDto);
    Task<ResponseDTO> DeleteAppointmentDeposit(string appointmentDepositId);
    Task<ResponseDTO> GetAppointmentByUserId(ClaimsPrincipal User);
    Task<ResponseDTO> GetAppointmentMeetLinkByUserId(ClaimsPrincipal User);
}