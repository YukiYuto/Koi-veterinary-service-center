using System.Security.Claims;
using KoiVeterinaryServiceCenter.Models.DTO;
using KoiVeterinaryServiceCenter.Models.DTO.Appointment;
using KoiVeterinaryServiceCenter.Models.DTO.AppointmentPet;

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

    Task<ResponseDTO> GetAppointmentPetByPetId(ClaimsPrincipal User, Guid petId);
    Task<ResponseDTO> GetAppointmentPetByAppointmentId(ClaimsPrincipal User, Guid appointmentId);
    Task<ResponseDTO> CreateAppointmentPet(ClaimsPrincipal User, CreateAppointmentPetDTO createAppointmentPetDto);
    //Task<ResponseDTO> DeleteAppointmentPet(string customerId);
    //Task<ResponseDTO> GetAppointmentByUserId(ClaimsPrincipal User);
    //Task<ResponseDTO> GetAppointmentMeetLinkByUserId(ClaimsPrincipal User);
}