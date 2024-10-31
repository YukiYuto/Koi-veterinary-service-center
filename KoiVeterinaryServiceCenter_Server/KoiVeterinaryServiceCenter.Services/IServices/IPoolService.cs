using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using KoiVeterinaryServiceCenter.Models.Domain;
using KoiVeterinaryServiceCenter.Models.DTO;
using KoiVeterinaryServiceCenter.Models.DTO.Pool;
using Microsoft.AspNetCore.Http;

namespace KoiVeterinaryServiceCenter.Services.IServices
{
    public interface IPoolService
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
        Task<ResponseDTO> CreatePool(ClaimsPrincipal User, CreatePoolDTO createPoolDTO);
        Task<ResponseDTO> GetPoolById(ClaimsPrincipal User, Guid poolId);
        Task<ResponseDTO> UpdatePool(ClaimsPrincipal User, UpdatePoolDTO updatePoolDTO);
        Task<ResponseDTO> DeletePoolById(ClaimsPrincipal User, Guid poolId);
        Task<ResponseDTO> UploadPoolAvatar(IFormFile file, ClaimsPrincipal user);
    }
}
