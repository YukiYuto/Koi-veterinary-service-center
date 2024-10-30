using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using KoiVeterinaryServiceCenter.Models.DTO.Pool;
using KoiVeterinaryServiceCenter.Models.DTO;

namespace KoiVeterinaryServiceCenter.Services.IServices
{
    interface IPetServiceService
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
        Task<ResponseDTO> CreatePetService(ClaimsPrincipal User, CreatePoolDTO createPoolDTO);
        Task<ResponseDTO> GetPetService(ClaimsPrincipal User, Guid poolId);
        Task<ResponseDTO> UpdatePetService(ClaimsPrincipal User, UpdatePoolDTO updatePoolDTO);
        Task<ResponseDTO> DeletePetService(ClaimsPrincipal User, Guid poolId);
    }
}
