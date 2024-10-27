using KoiVeterinaryServiceCenter.Models.DTO;
using KoiVeterinaryServiceCenter.Models.DTO.DoctorRating;
using System.Security.Claims;
using System.Threading.Tasks;

namespace KoiVeterinaryServiceCenter.Services.IServices
{
    public interface IDoctorRatingService
    {
        Task<ResponseDTO> GetAllRating(
               ClaimsPrincipal User,
               string? filterOn,
               string? filterQuery,
               string? sortBy,
               bool? isAscending,
               int pageNumber,
               int pageSize
           );
        Task<ResponseDTO> GetRatingByDoctorId(Guid doctorId); // Get all ratings + feedback
        Task<ResponseDTO> GetRatesByDoctorId(Guid doctorId); // Get average rating by doctor
        Task<ResponseDTO> GetAllRates(); // Get average rating across all doctors

        Task<ResponseDTO> CreateRating(ClaimsPrincipal User, CreateDoctorRatingDTO createDoctorRatingDTO);
        Task<ResponseDTO> UpdateRating(ClaimsPrincipal User, UpdateDoctorRatingDTO updateDoctorRatingDTO);
        Task<ResponseDTO> DeleteRating(ClaimsPrincipal user,   Guid doctorRatingId);
    }
}
