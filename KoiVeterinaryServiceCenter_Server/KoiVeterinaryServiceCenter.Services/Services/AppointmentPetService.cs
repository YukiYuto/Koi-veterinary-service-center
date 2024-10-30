using System.Security.Claims;
using KoiVeterinaryServiceCenter.Models.DTO;
using KoiVeterinaryServiceCenter.Models.DTO.Appointment;
using KoiVeterinaryServiceCenter.Services.IServices;

namespace KoiVeterinaryServiceCenter.Services.Services;

public class AppointmentPetService : IAppointmentPetService
{
    public Task<ResponseDTO> GetAppointmentPets
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

    public Task<ResponseDTO> GetAppointmentPetById(ClaimsPrincipal User, Guid appointmentPetId)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseDTO> CreateAppointmentPet(ClaimsPrincipal User, CreateAppointmentDTO createAppointmentPetDto)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseDTO> DeleteAppointmentPet(string customerId)
    {
        throw new NotImplementedException();
    }
}