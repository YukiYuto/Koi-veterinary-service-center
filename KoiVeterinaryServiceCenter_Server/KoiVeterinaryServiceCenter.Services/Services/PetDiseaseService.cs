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
                    PetName = pd.Pet.Name,
                    PetAge = pd.Pet.Age,
                    PetSpecies = pd.Pet.Species,
                    PetBreed = pd.Pet.Breed,
                    PetGender = pd.Pet.Gender,
                    DiseaseId = pd.DiseaseId,
                    DiseaseName = pd.Disease.DiseaseName,
                    Symptoms =pd.Disease.DiseaseSymptoms,
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

        public async Task<ResponseDTO> GetAllPetDisease(
      string? filterOn,
      string? filterQuery,
      string? sortBy,
      bool? isAscending,
      int pageNumber = 1,
      int pageSize = 10)
        {
            try
            {
                List<PetDisease> petDiseases = new List<PetDisease>();

                // Get all pet diseases from the repository
                
                var allPetDiseases = await _unitOfWork.PetDiseaseRepository.GetAllAsync(includeProperties: "Pet,Disease");

                // Filtering
                if (!string.IsNullOrEmpty(filterOn) && !string.IsNullOrEmpty(filterQuery))
                {
                    switch (filterOn.Trim().ToLower())
                    {
                        case "petid":
                            if (Guid.TryParse(filterQuery, out Guid petId))
                            {
                                petDiseases = allPetDiseases.Where(pd => pd.PetId == petId).ToList();
                            }
                            break;

                        case "diseaseid":
                            if (Guid.TryParse(filterQuery, out Guid diseaseId))
                            {
                                petDiseases = allPetDiseases.Where(pd => pd.DiseaseId == diseaseId).ToList();
                            }
                            break;

                        default:
                            petDiseases = allPetDiseases.ToList(); // If no valid filter, return all
                            break;
                    }
                }
                else
                {
                    petDiseases = allPetDiseases.ToList(); // If no filters, return all
                }

                // Sorting
                if (!string.IsNullOrEmpty(sortBy))
                {
                    switch (sortBy.Trim().ToLower())
                    {
                        case "petname":
                            petDiseases = isAscending == true
                                ? petDiseases.OrderBy(pd => pd.Pet.Name).ToList()
                                : petDiseases.OrderByDescending(pd => pd.Pet.Name).ToList();
                            break;

                        case "diseasename":
                            petDiseases = isAscending == true
                                ? petDiseases.OrderBy(pd => pd.Disease.DiseaseName).ToList()
                                : petDiseases.OrderByDescending(pd => pd.Disease.DiseaseName).ToList();
                            break;

                        default:
                            break; // Do not sort if no valid sort parameter
                    }
                }

                // Pagination
                if (pageNumber > 0 && pageSize > 0)
                {
                    var skipResult = (pageNumber - 1) * pageSize;
                    petDiseases = petDiseases.Skip(skipResult).Take(pageSize).ToList();
                }

                if (petDiseases == null || !petDiseases.Any())
                {
                    return new ResponseDTO()
                    {
                        Message = "No pet diseases found.",
                        Result = null,
                        IsSuccess = false,
                        StatusCode = 404
                    };
                }

                return new ResponseDTO()
                {
                    Message = "Pet diseases retrieved successfully.",
                    Result = petDiseases,
                    IsSuccess = true,
                    StatusCode = 200
                };
            }
            catch (Exception e)
            {
                return new ResponseDTO()
                {
                    Message = e.Message,
                    Result = null,
                    IsSuccess = false,
                    StatusCode = 500
                };
            }
        }


    }
}
