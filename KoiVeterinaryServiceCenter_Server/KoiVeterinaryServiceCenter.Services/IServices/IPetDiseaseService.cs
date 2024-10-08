using KoiVeterinaryServiceCenter.Model.DTO;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace KoiVeterinaryServiceCenter.Services.IServices
{
    public interface IPetDiseaseService
    {
        
        Task<ResponseDTO> GetPetDiseasesByPetId(ClaimsPrincipal User, Guid petId);

        Task<ResponseDTO> AddPetDisease(ClaimsPrincipal User, AddPetDiseaseDTO addPetDiseaseDto);

       
        Task<ResponseDTO> UpdatePetDisease(ClaimsPrincipal User, UpdatePetDiseaseDTO updatePetDiseaseDto);

   
        Task<ResponseDTO> DeletePetDisease(ClaimsPrincipal User, Guid petDiseaseId);

        Task<ResponseDTO> GetAllPetDisease();
    }
}
