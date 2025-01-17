﻿using AutoMapper;
using FirebaseAdmin.Messaging;
using KoiVeterinaryServiceCenter.DataAccess.IRepository;
using KoiVeterinaryServiceCenter.Models.Domain;
using KoiVeterinaryServiceCenter.Models.DTO;
using KoiVeterinaryServiceCenter.Models.DTO.Pet;
using KoiVeterinaryServiceCenter.Services.IServices;
using KoiVeterinaryServiceCenter.Utility.Constants;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace KoiVeterinaryServiceCenter.Services.Services
{
    public class PetsService : IPetService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFirebaseService _firebaseService;
        private readonly IMapper _mapper;

        public PetsService(IFirebaseService firebaseService, IUnitOfWork unitOfWork)
        {
            _firebaseService = firebaseService;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseDTO> CreatePet(ClaimsPrincipal user, CreatePetDTO createPetDTO)
        {
            var pet = new Pet
            {
                PetId = Guid.NewGuid(),
                Name = createPetDTO.Name,
                Species = createPetDTO.Species,
                Description = createPetDTO.Description,
                PetUrl= createPetDTO.PetUrl,
                CustomerId= createPetDTO.CustomerId
             
            };

            await _unitOfWork.PetRepository.AddAsync(pet);
            await _unitOfWork.SaveAsync();

            return new ResponseDTO
            {
                IsSuccess = true,
                Message = "Pet created successfully",
                StatusCode = 200,
                Result = pet
            };
        }

        public async Task<ResponseDTO> GetAllPets(
            ClaimsPrincipal user,
            string? filterOn,
            string? filterQuery,
            string? sortBy,
            bool? isAscending,
            int pageNumber,
            int pageSize)
        {
            try
            {
                var pets = await _unitOfWork.PetRepository.GetAllAsync();

                if (!string.IsNullOrEmpty(filterOn) && !string.IsNullOrEmpty(filterQuery))
                {
                    pets = filterOn.ToLower() switch
                    {
                        "name" => pets.Where(p => p.Name.Contains(filterQuery, StringComparison.OrdinalIgnoreCase)).ToList(),
                        "species" => pets.Where(p => p.Species != null &&
                                                     p.Species.Contains(filterQuery, StringComparison.OrdinalIgnoreCase)).ToList(),
                        "description" => pets.Where(p => p.Description != null &&
                                                   p.Description.Contains(filterQuery, StringComparison.OrdinalIgnoreCase)).ToList(),
                        _ => pets
                    };
                }

                if (!string.IsNullOrEmpty(sortBy))
                {
                    pets = sortBy.ToLower() switch
                    {
                        "name" => isAscending == true ? pets.OrderBy(p => p.Name).ToList() : pets.OrderByDescending(p => p.Name).ToList(),
                        "species" => isAscending == true ? pets.OrderBy(p => p.Species).ToList() : pets.OrderByDescending(p => p.Species).ToList(),
                        _ => pets
                    };
                }

                var skip = (pageNumber - 1) * pageSize;
                pets = pets.Skip(skip).Take(pageSize).ToList();

                if (!pets.Any())
                {
                    return new ResponseDTO
                    {
                        IsSuccess = false,
                        Message = "No pets found",
                        StatusCode = 404
                    };
                }

                var petDtos = pets.Select(p => new GetPetDTO
                {
                    PetId = p.PetId,
                    Name = p.Name,
                    Species = p.Species,
                    Description = p.Description,
                    Customer = p.CustomerId,
                    PetUrl = p.PetUrl,
                }).ToList();

                return new ResponseDTO
                {
                    IsSuccess = true,
                    Message = "Pets retrieved successfully",
                    StatusCode = 200,
                    Result = petDtos
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    StatusCode = 500
                };
            }
        }

        public async Task<ResponseDTO> GetPetById(Guid petId)
        {
            var pet = await _unitOfWork.PetRepository.GetByIdAsync(petId);

            if (pet == null)
            {
                return new ResponseDTO
                {
                    IsSuccess = false,
                    Message = "Pet not found",
                    StatusCode = 404
                };
            }

            var petDto = new GetPetDTO
            {
                PetId = pet.PetId,
                Name = pet.Name,
                Species = pet.Species,
                Description = pet.Description,
                Customer = pet.CustomerId,
                PetUrl= pet.PetUrl,
               
                
            };

            return new ResponseDTO
            {
                IsSuccess = true,
                Message = "Pet retrieved successfully",
                StatusCode = 200,
                Result = petDto
            };
        }

        public async Task<ResponseDTO> GetPetsByCustomerId(ClaimsPrincipal user, string customerId)
        {
            List<Pet> pets = await _unitOfWork.PetRepository.GetPetbyCustomerId(customerId);

            if (pets == null || !pets.Any())
            {
                return new ResponseDTO
                {
                    IsSuccess = false,
                    Message = "No pets found for the given customer",
                    StatusCode = 404
                };
            }


            return new ResponseDTO
            {
                IsSuccess = true,
                Message = "Pets retrieved successfully",
                StatusCode = 200,
                Result = pets
            };
        }

        public async Task<ResponseDTO> UpdatePet(ClaimsPrincipal user, UpdatePetDTO updatePetDTO)
        {
             var pet = await _unitOfWork.PetRepository.GetByIdAsync(updatePetDTO.PetId);

             if (pet == null)
             {
                 return new ResponseDTO
                 {
                     IsSuccess = false,
                     Message = "Pet not found",
                     StatusCode = 404
                 };
             }

             pet.Name = updatePetDTO.Name ?? pet.Name;
             pet.Species = updatePetDTO.Species ?? pet.Species;
             pet.Description = updatePetDTO.Description ?? pet.Description;
             pet.PetUrl = updatePetDTO.PetUrl ?? pet.PetUrl;

             _unitOfWork.PetRepository.Update(pet);
             await _unitOfWork.SaveAsync();
            return new ResponseDTO
            {
                IsSuccess = true,
                Message = "Pet updated successfully",
                StatusCode = 200,
                Result = pet
            };

        }

        public async Task<ResponseDTO> DeletePet(ClaimsPrincipal user, Guid petId)
        {
            var pet = await _unitOfWork.PetRepository.GetByIdAsync(petId);

            if (pet == null)
            {
                return new ResponseDTO
                {
                    IsSuccess = false,
                    Message = "Pet not found",
                    StatusCode = 404
                };
            }

            _unitOfWork.PetRepository.Remove(pet);
            await _unitOfWork.SaveAsync();

            return new ResponseDTO
            {
                IsSuccess = true,
                Message = "Pet deleted successfully",
                StatusCode = 200
            };
        }
        public async Task<ResponseDTO> UploadPetAvatar(IFormFile file, ClaimsPrincipal user)
        {
            if (file == null)
            {
                return new ResponseDTO()
                {
                    IsSuccess = false,
                    StatusCode = 400,
                    Message = "No file uploaded."
                };
            }

            // Upload image lên Firebase và nhận URL công khai
            var responseDto = await _firebaseService.UploadImagePet(file, StaticFirebaseFolders.PetAvatars);

            if (!responseDto.IsSuccess)
            {
                return new ResponseDTO()
                {
                    Message = "Image upload failed!",
                    Result = null,
                    IsSuccess = false,
                    StatusCode = 400 // Bad Request
                };
            }

            // Trả về link công khai của hình ảnh
            return new ResponseDTO()
            {
                Message = "Upload post image successfully!",
                Result = responseDto.Result, // Đảm bảo đây là URL công khai của ảnh đã upload
                IsSuccess = true,
                StatusCode = 200 // OK
            };
        }
    }
}
