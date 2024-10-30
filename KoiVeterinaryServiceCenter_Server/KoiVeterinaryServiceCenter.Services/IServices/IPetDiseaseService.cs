using KoiVeterinaryServiceCenter.Models.DTO;
using KoiVeterinaryServiceCenter.Models.DTO.Pet; // Ensure you have a DTO for Pet
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace KoiVeterinaryServiceCenter.Services.IServices
{
    public interface IPetDiseaseService
    {
        Task<ResponseDTO> GetAllPetDiseases(
          ClaimsPrincipal user,
          string? filterOn,
          string? filterQuery,
          string? sortBy,
          bool? isAscending,
          int pageNumber,
          int pageSize
      );

        Task<ResponseDTO> GetPetDiseasesByPetId(Guid petId);

        Task<ResponseDTO> GetPetDiseasesByDiseaseId(Guid diseaseId);

        Task<ResponseDTO> AddPetDisease(ClaimsPrincipal user, CreatePetDiseaseDTO petDiseaseDTO);

        Task<ResponseDTO> UpdatePetDisease(ClaimsPrincipal user, UpdatePetDiseaseDTO petDiseaseDTO);

        Task<ResponseDTO> DeletePetDisease(ClaimsPrincipal user, Guid petId, Guid diseaseId);
    }
}
