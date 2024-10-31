using KoiVeterinaryServiceCenter.Models.DTO;
using KoiVeterinaryServiceCenter.Models.DTO.Pet; // Ensure you have a DTO for Post
using KoiVeterinaryServiceCenter.Models.DTO;
using KoiVeterinaryServiceCenter.Models.DTO.Pet; // Ensure you have a DTO for Post
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Threading.Tasks;


namespace KoiVeterinaryServiceCenter.Services.IServices
{
    public interface IPetService
    {
        Task<ResponseDTO> GetAllPets(
            ClaimsPrincipal user,
            string? filterOn,
            string? filterQuery,
            string? sortBy,
            bool? isAscending,
            int pageNumber,
            int pageSize
        );

        Task<ResponseDTO> GetPetById(Guid petId); 

        Task<ResponseDTO> GetPetsByCustomerId(ClaimsPrincipal user, string customerId);

        Task<ResponseDTO> CreatePet(ClaimsPrincipal user, CreatePetDTO createPetDTO); 
        Task<ResponseDTO> UpdatePet(ClaimsPrincipal user, UpdatePetDTO updatePostDTO); 
        Task<ResponseDTO> DeletePet(ClaimsPrincipal user, Guid petId);
        Task<ResponseDTO> UploadPetAvatar(IFormFile file, ClaimsPrincipal user);
    }
}
