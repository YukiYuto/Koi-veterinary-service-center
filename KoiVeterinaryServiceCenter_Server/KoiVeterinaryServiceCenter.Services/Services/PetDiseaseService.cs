using KoiVeterinaryServiceCenter.DataAccess.IRepository;
using KoiVeterinaryServiceCenter.Model.Domain;
using KoiVeterinaryServiceCenter.Model.DTO;
using KoiVeterinaryServiceCenter.Services.IServices;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace KoiVeterinaryServiceCenter.Services.Services
{
    public class PetDiseaseService : IPetDiseaseService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PetDiseaseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // Add a disease to a pet
        public async Task<ResponseDTO> AddPetDisease(ClaimsPrincipal user, AddPetDiseaseDTO addPetDiseaseDto)
        {
            try
            {
                var pet = await _unitOfWork.PetRepository.GetPetById(addPetDiseaseDto.PetId);

                if (pet == null)
                {
                    return new ResponseDTO
                    {
                        IsSuccess = false,
                        StatusCode = 404,
                        Message = "Pet not found"
                    };
                }

                var disease = await _unitOfWork.DiseaseRepository.GetDiseaseById(addPetDiseaseDto.DiseaseId);

                if (disease == null)
                {
                    return new ResponseDTO
                    {
                        IsSuccess = false,
                        StatusCode = 404,
                        Message = "Disease not found"
                    };
                }

                var newPetDisease = new PetDisease
                {
                    PetId = addPetDiseaseDto.PetId,
                    DiseaseId = addPetDiseaseDto.DiseaseId,
                    Description = addPetDiseaseDto.Description,
                    Date = addPetDiseaseDto.Date
                };

                await _unitOfWork.PetDiseaseRepository.AddAsync(newPetDisease);
                await _unitOfWork.SaveAsync();

                return new ResponseDTO
                {
                    IsSuccess = true,
                    StatusCode = 200,
                    Message = "Pet disease added successfully"
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

        // Get all diseases of a pet
        public async Task<ResponseDTO> GetPetDiseasesByPetId(ClaimsPrincipal user, Guid petId)
        {
            try
            {
                var petDiseases = await _unitOfWork.PetDiseaseRepository.GetAllAsync(pd => pd.PetId == petId, includeProperties: "Disease,Pet");

                if (petDiseases == null || !petDiseases.Any())
                {
                    return new ResponseDTO
                    {
                        IsSuccess = false,
                        StatusCode = 404,
                        Message = "No diseases found for this pet"
                    };
                }

                var diseaseDTOs = petDiseases.Select(pd => new GetPetDiseaseDTO
                {
                    PetId = pd.PetId,
                    DiseaseId = pd.DiseaseId,
                    DiseaseName = pd.Disease.DiseaseName,
                    Description = pd.Description,
                    Date = pd.Date
                }).ToList();

                return new ResponseDTO
                {
                    IsSuccess = true,
                    StatusCode = 200,
                    Result = diseaseDTOs
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

        // Update a pet's disease
        public async Task<ResponseDTO> UpdatePetDisease(ClaimsPrincipal user, UpdatePetDiseaseDTO updatePetDiseaseDto)
        {
            try
            {
                var existingPetDisease = await _unitOfWork.PetDiseaseRepository.GetAsync(pd => pd.PetId == updatePetDiseaseDto.PetId && pd.DiseaseId == updatePetDiseaseDto.DiseaseId);

                if (existingPetDisease == null)
                {
                    return new ResponseDTO
                    {
                        IsSuccess = false,
                        StatusCode = 404,
                        Message = "Pet disease not found"
                    };
                }

                existingPetDisease.Description = updatePetDiseaseDto.Description;
                existingPetDisease.Date = updatePetDiseaseDto.Date;

                _unitOfWork.PetDiseaseRepository.Update(existingPetDisease);
                await _unitOfWork.SaveAsync();

                return new ResponseDTO
                {
                    IsSuccess = true,
                    StatusCode = 200,
                    Message = "Pet disease updated successfully"
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

        // Delete a pet disease
        public async Task<ResponseDTO> DeletePetDisease(ClaimsPrincipal user, Guid petDiseaseId)
        {
            try
            {
                var petDisease = await _unitOfWork.PetDiseaseRepository.GetPetDiseaseById(petDiseaseId);

                if (petDisease == null)
                {
                    return new ResponseDTO
                    {
                        IsSuccess = false,
                        StatusCode = 404,
                        Message = "Pet disease not found"
                    };
                }

                _unitOfWork.PetDiseaseRepository.Remove(petDisease);
                await _unitOfWork.SaveAsync();

                return new ResponseDTO
                {
                    IsSuccess = true,
                    StatusCode = 200,
                    Message = "Pet disease deleted successfully"
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
    }
}
