using KoiVeterinaryServiceCenter.Models.DTO;

using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace KoiVeterinaryServiceCenter.Services.IServices
{
    public interface IDiseaseService
    {
        Task<ResponseDTO> CreateDisease(ClaimsPrincipal user, CreateDiseaseDTO createDiseaseDTO);
        Task<ResponseDTO> UpdateDisease(ClaimsPrincipal user, UpdateDiseaseDTO updateDiseaseDTO);
        Task<ResponseDTO> GetDiseaseById(Guid diseaseId);
        Task<ResponseDTO> GetAllDiseases(string? filterOn, string? filterQuery, string? sortBy, bool? isAscending, int pageNumber, int pageSize);
        Task<ResponseDTO> DeleteDisease(ClaimsPrincipal user, Guid diseaseId);
    }
}
