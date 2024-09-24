using KoiVeterinaryServiceCenter.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace KoiVeterinaryServiceCenter.Services.IServices
{
    public interface IPetDiseaseServices
    {
        Task<ResponseDTO> GetPetDiseases
             (
            ClaimsPrincipal User,
            string? filterOn,
            string? filterQuery,
            string? sortBy,
            bool? isAscending,
            int pageNumber = 0,
            int pageSize = 0
        );
        Task<ResponseDTO> GetPetDiseasesByPetId(ClaimsPrincipal User, Guid petId);

        Task<ResponseDTO> CreatePetDisease(ClaimsPrincipal User, CreatePetDiseaseDTO createPetDiseaseDto);

        Task<ResponseDTO> UpdatePetDisease(ClaimsPrincipal User, UpdatePetDiseaseDTO updatePetDiseaseDto);

        Task<ResponseDTO> DeletePetDisease(ClaimsPrincipal User, Guid petDiseaseId);

    }
}
