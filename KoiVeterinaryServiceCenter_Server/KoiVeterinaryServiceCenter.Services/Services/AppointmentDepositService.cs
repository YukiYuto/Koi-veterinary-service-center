using System.Security.Claims;
using KoiVeterinaryServiceCenter.Models.DTO;
using KoiVeterinaryServiceCenter.Models.DTO.Appointment;
using KoiVeterinaryServiceCenter.Models.DTO.AppointmentPet;
using KoiVeterinaryServiceCenter.Services.IServices;

namespace KoiVeterinaryServiceCenter.Services.Services;

public class AppointmentDepositService : IAppointmentDepositService
{
    public Task<ResponseDTO> GetAppointmentDeposits
    (ClaimsPrincipal User,
        string? filterOn,
        string? filterQuery,
        string? sortBy,
        int pageNumber = 0,
        int pageSize = 0
    )
    {
        throw new NotImplementedException();
    }

    public Task<ResponseDTO> GetAppointmentDepositById(ClaimsPrincipal User, Guid appointmentId)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseDTO> CreateAppointmentDeposit(ClaimsPrincipal User,
        CreateAppointmentDepositDTO createAppointmentDepositDto)
    {
        /*var user = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        if (user != createAppointmentDepositDto.CustomerId)
        {
            return new ResponseDTO()
            {
                Result = "",
                Message = "You do not have permission to create this appointment.",
                IsSuccess = false,
                StatusCode = 403
            };
        }
        {
            return new ResponseDTO()
            {
                Result = "",
                Message = "You do not have permission to create this appointment.",
                IsSuccess = false,
                StatusCode = 403
            };
        }*/
        return null;

    }

    public Task<ResponseDTO> DeleteAppointmentDeposit(string appointmentDepositId)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseDTO> GetAppointmentByUserId(ClaimsPrincipal User)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseDTO> GetAppointmentMeetLinkByUserId(ClaimsPrincipal User)
    {
        throw new NotImplementedException();
    }
}