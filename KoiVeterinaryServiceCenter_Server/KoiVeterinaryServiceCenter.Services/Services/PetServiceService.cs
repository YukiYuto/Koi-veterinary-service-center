using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using KoiVeterinaryServiceCenter.Models.DTO;
using KoiVeterinaryServiceCenter.Models.DTO.Pool;
using KoiVeterinaryServiceCenter.Services.IServices;

namespace KoiVeterinaryServiceCenter.Services.Services
{
    public class PetServiceService : IPetServiceService
    {
        public Task<ResponseDTO> CreatePetService(ClaimsPrincipal User, CreatePoolDTO createPoolDTO)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDTO> DeletePetService(ClaimsPrincipal User, Guid poolId)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDTO> GetAll(ClaimsPrincipal User, string? filterOn, string? filterQuery, string? sortBy, bool? isAscending, int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDTO> GetPetService(ClaimsPrincipal User, Guid poolId)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDTO> UpdatePetService(ClaimsPrincipal User, UpdatePoolDTO updatePoolDTO)
        {
            throw new NotImplementedException();
        }
    }
}
