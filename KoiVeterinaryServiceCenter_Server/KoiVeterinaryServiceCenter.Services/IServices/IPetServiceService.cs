using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using KoiVeterinaryServiceCenter.Models.DTO;
using KoiVeterinaryServiceCenter.Models.DTO.PetService;

namespace KoiVeterinaryServiceCenter.Services.IServices
{
    public interface IPetServiceService
    {
        Task<ResponseDTO> GetAll(
                ClaimsPrincipal User,
                string? filterOn,
                string? filterQuery,
                string? sortBy,
                bool? isAscending,
                int pageNumber,
                int pageSize
            );
        Task<ResponseDTO> CreatePetService(ClaimsPrincipal User, CreatePetServiceDTO createPetServiceDTO);
        Task<ResponseDTO> GetPetServiceById(ClaimsPrincipal User, Guid petServiceId);
        Task<ResponseDTO> UpdatePetService(ClaimsPrincipal User, UpdatePetServiceDTO updatePetServiceDTO);
        Task<ResponseDTO> DeletePetService(ClaimsPrincipal User, Guid petServiceId);
    }
}
