using AutoMapper;
using KoiVeterinaryServiceCenter.DataAccess.IRepository;
using KoiVeterinaryServiceCenter.Model.Domain;
using KoiVeterinaryServiceCenter.Model.DTO;
using KoiVeterinaryServiceCenter.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace KoiVeterinaryServiceCenter.Services.Services
{
    public class PetServices : IPetServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public PetServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseDTO> CreatePet(ClaimsPrincipal User, CreatePetDTO createPetDto)
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
                if (userId == null)
                {
                    return new ResponseDTO
                    {
                        Message = "User is not authenticated",
                        Result = null,
                        IsSuccess = false,
                        StatusCode = 401
                    };
                }

                Pet pets = new Pet
                {
                    CustomerId = createPetDto.CustomerId,
                    Name = createPetDto.Name,
                    Age = createPetDto.Age,
                    Species = createPetDto.Species,
                    Breed = createPetDto.Breed,
                    Gender = createPetDto.Gender
                };

                // Add new Pet
                await _unitOfWork.PetRepository.AddAsync(pets);
                await _unitOfWork.SaveAsync();

                return new ResponseDTO
                {
                    Message = "Pet created successfully",
                    Result = pets,
                    IsSuccess = true,
                    StatusCode = 200
                };
            }
            catch (Exception e)
            {
                return new ResponseDTO
                {
                    Message = e.Message,
                    Result = null,
                    IsSuccess = false,
                    StatusCode = 500
                };
            }
        }

        public async Task<ResponseDTO> DeletePet(ClaimsPrincipal User, Guid petId)
        {
            try
            {
                var pet = await _unitOfWork.PetRepository.GetPetById(petId);
                if (pet == null)
                {
                    return new ResponseDTO
                    {
                        Message = "Pet not found",
                        IsSuccess = false,
                        StatusCode = 404
                    };
                }

                _unitOfWork.PetRepository.Remove(pet);
                await _unitOfWork.SaveAsync();

                return new ResponseDTO
                {
                    Message = "Pet deleted successfully",
                    IsSuccess = true,
                    StatusCode = 200
                };
            }
            catch (Exception e)
            {
                return new ResponseDTO
                {
                    Message = e.Message,
                    IsSuccess = false,
                    StatusCode = 500
                };
            }
        }

        public async Task<ResponseDTO> GetPet(ClaimsPrincipal User, Guid petId)
        {
            try
            {
                var pet = await _unitOfWork.PetRepository.GetPetById(petId);
                if (pet == null)
                {
                    return new ResponseDTO
                    {
                        Message = "Pet not found",
                        IsSuccess = false,
                        StatusCode = 404
                    };
                }

                var petDto = _mapper.Map<CreatePetDTO>(pet);
                return new ResponseDTO
                {
                    Message = "Pet retrieved successfully",
                    Result = petDto,
                    IsSuccess = true,
                    StatusCode = 200
                };
            }
            catch (Exception e)
            {
                return new ResponseDTO
                {
                    Message = e.Message,
                    IsSuccess = false,
                    StatusCode = 500
                };
            }
        }

        public Task<ResponseDTO> GetPets(ClaimsPrincipal User, string? filterOn, string? filterQuery, string? sortBy, bool? isAscending, int pageNumber = 0, int pageSize = 0)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDTO> UpdatePet(ClaimsPrincipal User, UpdatePetDTO updatePetDto)
        {
            throw new NotImplementedException();
        }
    }
}
