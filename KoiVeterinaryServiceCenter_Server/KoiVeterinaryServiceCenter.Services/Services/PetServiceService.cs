using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using KoiVeterinaryServiceCenter.DataAccess.IRepository;
using KoiVeterinaryServiceCenter.Models.Domain;
using KoiVeterinaryServiceCenter.Models.DTO;
using KoiVeterinaryServiceCenter.Models.DTO.PetService;
using KoiVeterinaryServiceCenter.Models.DTO.Pool;
using KoiVeterinaryServiceCenter.Services.IServices;

namespace KoiVeterinaryServiceCenter.Services.Services
{
    public class PetServiceService : IPetServiceService
    {
        private readonly IUnitOfWork _unitOfWork;
        public PetServiceService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ResponseDTO> CreatePetService(ClaimsPrincipal User, CreatePetServiceDTO createPetServiceDTO)
        {
            try
            {
                PetService petService = new PetService()
                {
                    PetId = createPetServiceDTO.PetId,
                    ServiceId = createPetServiceDTO.ServiceId
                };

                await _unitOfWork.PetServiceRepository.AddAsync(petService);
                await _unitOfWork.SaveAsync();

                return new ResponseDTO()
                {
                    Message = "Add pet service successfully",
                    IsSuccess = true,
                    StatusCode = 200,
                    Result = null
                };
            }
            catch (Exception e)
            {
                return new ResponseDTO()
                {
                    Message = e.Message,
                    IsSuccess = false,
                    StatusCode = 500,
                    Result = null
                };
            }
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

        public async Task<ResponseDTO> UpdatePetService(ClaimsPrincipal User, UpdatePetServiceDTO updatePetServiceDTO)
        {
            try
            {
                var petService = await _unitOfWork.PetServiceRepository.GetById(updatePetServiceDTO.PetId);
                if (petService is null)
                {
                    return new ResponseDTO()
                    {
                        Message = "Cannot found pet",
                        IsSuccess = false,
                        StatusCode = 404,
                        Result = null
                    };
                }

                petService.ServiceId = updatePetServiceDTO.ServiceId;
                var service = await _unitOfWork.ServiceRepository.GetServiceById(petService.ServiceId);
                if (service is null)
                {
                    return new ResponseDTO()
                    {
                        Message = "Cannot found service",
                        IsSuccess = false,
                        StatusCode = 404,
                        Result = null
                    };
                }

                _unitOfWork.PetServiceRepository.Update(petService);
                await _unitOfWork.SaveAsync();

                return new ResponseDTO()
                {
                    Message = "Update pet service successfully",
                    IsSuccess = true,
                    StatusCode = 200,
                    Result = null
                };
            }
            catch (Exception e)
            {
                return new ResponseDTO()
                {
                    Message = e.Message,
                    IsSuccess = false,
                    StatusCode = 500,
                    Result = null
                };
            }
        }
    }
}
