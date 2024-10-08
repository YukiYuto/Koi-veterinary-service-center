using AutoMapper;
using KoiVeterinaryServiceCenter.DataAccess.IRepository;
using KoiVeterinaryServiceCenter.DataAccess.Migrations;
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
    public class PetService : IPetService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public PetService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseDTO> CreatePet(ClaimsPrincipal User, CreatePetDTO createPetDto)
        {
            try
            {
                var userId = await _unitOfWork.CustomerRepository.GetAsync(u => u.Id == createPetDto.CustomerId);
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
                        Result = null,
                        Message = "Pet was not found",
                        IsSuccess = false,
                        StatusCode = 404
                    };
                }


                var petDto = new GetPetDTO
                {
                    PetId = pet.PetId,
                    CustomerId = pet.CustomerId,
                    Name = pet.Name,
                    Age = pet.Age,
                    Species = pet.Species,
                    Breed = pet.Breed,
                    Gender = pet.Gender
                };

                return new ResponseDTO
                {
                    Result = petDto,
                    Message = "Get pet successfully",
                    IsSuccess = true,
                    StatusCode = 200
                };
            }
            catch (Exception e)
            {
                return new ResponseDTO
                {
                    Result = null,
                    Message = e.Message,
                    IsSuccess = false,
                    StatusCode = 500
                };
            }
        }
        public async Task<ResponseDTO> GetAllPets()
        {
            try
            {
                var pets = await _unitOfWork.PetRepository.GetAllAsync(includeProperties: "ApplicationUser");

                if (pets == null || !pets.Any())
                {
                    return new ResponseDTO
                    {
                        IsSuccess = false,
                        StatusCode = 404,
                        Message = "No pets found"
                    };
                }

                // Map to DTOs (GetPetDTO)
                var petDTOs = pets.Select(pet => new GetPetDTO
                {
                    PetId = pet.PetId,
                    Name = pet.Name,
                    Age = pet.Age,
                    Species = pet.Species,
                    Breed = pet.Breed,
                    Gender = pet.Gender,
                   
                }).ToList();

                return new ResponseDTO
                {
                    IsSuccess = true,
                    StatusCode = 200,
                    Result = petDTOs
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    IsSuccess = false,
                    StatusCode = 500,
                    Message = ex.Message
                };
            }
        }

        public async Task<ResponseDTO> GetPetsByUserId(string userId)
        {
            try
            {
                // Assuming the CustomerId in your database corresponds to the userId here
                var pets = await _unitOfWork.PetRepository.GetAllAsync(p => p.CustomerId == userId, includeProperties: "ApplicationUser");

                if (pets == null || !pets.Any())
                {
                    return new ResponseDTO
                    {
                        IsSuccess = false,
                        StatusCode = 404,
                        Message = "No pets found for this customer"
                    };
                }

                // Map to DTOs (GetPetDTO)
                var petDTOs = pets.Select(pet => new GetPetsByCustomerIdDTO
                {
                    PetId = pet.PetId,
                    Name = pet.Name,
                    Age = pet.Age,
                    Species = pet.Species,
                    Breed = pet.Breed,
                    Gender = pet.Gender,
                    OwnerName = pet.ApplicationUser.UserName  // Assuming Customer has a Name property
                }).ToList();

                return new ResponseDTO
                {
                    IsSuccess = true,
                    StatusCode = 200,
                    Result = petDTOs
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    IsSuccess = false,
                    StatusCode = 500,
                    Message = ex.Message
                };
            }
        }

        public async Task<ResponseDTO> UpdatePet(Guid petId,  ClaimsPrincipal User, UpdatePetDTO updatePetDto)
        {
            try
            {
                // Retrieve the existing pet using the repository
                var existingPet = await _unitOfWork.PetRepository.GetPetById(petId);

                // Check if the pet exists
                if (existingPet == null)
                {
                    return new ResponseDTO
                    {
                        Result = null,
                        Message = "Pet not found",
                        IsSuccess = false,
                        StatusCode = 404
                    };
                }

               
            

                // Update properties of the existing pet (excluding CustomerId)
                existingPet.Name = updatePetDto.Name;
                existingPet.Age = updatePetDto.Age;
                existingPet.Species = updatePetDto.Species;
                existingPet.Breed = updatePetDto.Breed;
                existingPet.Gender = updatePetDto.Gender;

                // Save changes to the database
                _unitOfWork.PetRepository.Update(existingPet);
                await _unitOfWork.SaveAsync();

                return new ResponseDTO
                {
                    Result = existingPet,
                    Message = "Pet updated successfully",
                    IsSuccess = true,
                    StatusCode = 200
                };
            }
            catch (Exception e)
            {
                return new ResponseDTO
                {
                    Result = null,
                    Message = e.Message,
                    IsSuccess = false,
                    StatusCode = 500
                };
            }
        }


    }
}

