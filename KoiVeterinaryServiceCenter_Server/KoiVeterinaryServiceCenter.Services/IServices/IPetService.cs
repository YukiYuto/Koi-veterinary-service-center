using KoiVeterinaryServiceCenter.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace KoiVeterinaryServiceCenter.Services.IServices
{
    public interface IPetService
    {


        Task<ResponseDTO> GetAllPets(
      
       string? filterOn,
       string? filterQuery,
       string? sortBy,
       bool? isAscending,
       int pageNumber = 0,
       int pageSize = 0
   );

        Task<ResponseDTO> GetPetsByUserId(
           string UserId,
       string? filterOn,
       string? filterQuery,
       string? sortBy,
       bool? isAscending,
       int pageNumber = 0,
       int pageSize = 0
   );

        Task<ResponseDTO> GetPet(ClaimsPrincipal User, Guid petId);
        Task<ResponseDTO> CreatePet(ClaimsPrincipal User, CreatePetDTO createPetDto);

        Task<ResponseDTO> UpdatePet(Guid petId,  ClaimsPrincipal User, UpdatePetDTO updatePetDto);

        Task<ResponseDTO> DeletePet(ClaimsPrincipal User, Guid petId);
    }

}
