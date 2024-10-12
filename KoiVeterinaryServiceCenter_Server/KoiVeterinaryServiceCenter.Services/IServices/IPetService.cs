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


        Task<ResponseDTO> GetAllPets();

        Task<ResponseDTO> GetPetsByUserId(string userId);

        Task<ResponseDTO> GetPet(ClaimsPrincipal User, Guid petId);
        Task<ResponseDTO> CreatePet(ClaimsPrincipal User, CreatePetDTO createPetDto);

        Task<ResponseDTO> UpdatePet(Guid petId,  ClaimsPrincipal User, UpdatePetDTO updatePetDto);

        Task<ResponseDTO> DeletePet(ClaimsPrincipal User, Guid petId);
    }

}
