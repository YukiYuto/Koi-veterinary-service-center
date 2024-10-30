using System.Security.Claims;
using KoiVeterinaryServiceCenter.Models.DTO;
using KoiVeterinaryServiceCenter.Models.DTO.Appointment;

namespace KoiVeterinaryServiceCenter.Services.IServices;

public interface IAppointmentPetService
{
    Task<ResponseDTO> GetAppointmentPets
    (
        ClaimsPrincipal User,
        string? filterOn,
        string? filterQuery,
        string? sortBy,
        int pageNumber = 0,
        int pageSize = 0
    );

    Task<ResponseDTO> GetAppointmentPetById(ClaimsPrincipal User, Guid appointmentPetId);
    Task<ResponseDTO> CreateAppointmentPet(ClaimsPrincipal User, CreateAppointmentDTO createAppointmentPetDto);
    Task<ResponseDTO> DeleteAppointmentPet(string customerId);
    //Task<ResponseDTO> GetAppointmentByUserId(ClaimsPrincipal User);
    //Task<ResponseDTO> GetAppointmentMeetLinkByUserId(ClaimsPrincipal User);
}