using KoiVeterinaryServiceCenter.DataAccess.IRepository;
using KoiVeterinaryServiceCenter.Models.Domain;
using KoiVeterinaryServiceCenter.Models.DTO;
using KoiVeterinaryServiceCenter.Services.IServices;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace KoiVeterinaryServiceCenter.Services.Services
{
    public class DiseaseService : IDiseaseService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DiseaseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseDTO> CreateDisease(ClaimsPrincipal user, CreateDiseaseDTO createDiseaseDTO)
        {
            try
            {
                Disease disease = new Disease()
                {
                    Name = createDiseaseDTO.Name,
                    Description = createDiseaseDTO.Description,
                    Symptoms = createDiseaseDTO.Symptoms,
                    Medication = createDiseaseDTO.Medication
                };

                await _unitOfWork.diseaseRepository.AddAsync(disease);
                await _unitOfWork.SaveAsync();

                return new ResponseDTO()
                {
                    Message = "Disease created successfully",
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

        public async Task<ResponseDTO> DeleteDisease(ClaimsPrincipal user, Guid diseaseId)
        {
            try
            {
                var disease = await _unitOfWork.diseaseRepository.GetByIdAsync(diseaseId);
                if (disease is null)
                {
                    return new ResponseDTO()
                    {
                        Message = "Disease not found",
                        IsSuccess = false,
                        StatusCode = 404,
                        Result = null
                    };
                }

                _unitOfWork.diseaseRepository.Remove(disease);
                await _unitOfWork.SaveAsync();

                return new ResponseDTO()
                {
                    Message = "Disease deleted successfully",
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

        public async Task<ResponseDTO> GetAllDiseases(string? filterOn, string? filterQuery, string? sortBy, bool? isAscending, int pageNumber, int pageSize)
        {
            try
            {
                var diseases = await _unitOfWork.diseaseRepository.GetAllAsync();

                if (string.IsNullOrEmpty(filterOn) || string.IsNullOrEmpty(filterQuery))
                {
                    // No filtering applied
                }
                else
                {
                    // Apply filtering based on filterOn and filterQuery
                    switch (filterOn.Trim().ToLower())
                    {
                        case "name":
                            diseases = diseases.Where(d => d.Name.Contains(filterQuery, StringComparison.OrdinalIgnoreCase)).ToList();
                            break;
                            // Add more filtering cases if necessary
                    }
                }

                // Sorting
                if (!string.IsNullOrEmpty(sortBy))
                {
                    diseases = sortBy.Trim().ToLower() switch
                    {
                        "name" => isAscending == true ? diseases.OrderBy(d => d.Name).ToList() : diseases.OrderByDescending(d => d.Name).ToList(),
                        _ => diseases
                    };
                }

                // Pagination
                if (pageNumber > 0 && pageSize > 0)
                {
                    var skip = (pageNumber - 1) * pageSize;
                    diseases = diseases.Skip(skip).Take(pageSize).ToList();
                }

                return new ResponseDTO()
                {
                    Message = "Retrieved all diseases successfully",
                    IsSuccess = true,
                    StatusCode = 200,
                    Result = diseases // You may want to map this to DTOs if needed
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

        public async Task<ResponseDTO> GetDiseaseById(Guid diseaseId)
        {
            try
            {
                var disease = await _unitOfWork.diseaseRepository.GetByIdAsync(diseaseId);
                if (disease is null)
                {
                    return new ResponseDTO()
                    {
                        Message = "Disease not found",
                        IsSuccess = false,
                        StatusCode = 404,
                        Result = null
                    };
                }

                return new ResponseDTO()
                {
                    Message = "Disease retrieved successfully",
                    IsSuccess = true,
                    StatusCode = 200,
                    Result = disease // You may want to map this to a DTO
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

        public async Task<ResponseDTO> UpdateDisease(ClaimsPrincipal user, UpdateDiseaseDTO updateDiseaseDTO)
        {
            try
            {
                var disease = await _unitOfWork.diseaseRepository.GetByIdAsync(updateDiseaseDTO.DiseaseId);
                if (disease is null)
                {
                    return new ResponseDTO()
                    {
                        Message = "Disease not found",
                        IsSuccess = false,
                        StatusCode = 404,
                        Result = null
                    };
                }

                // Update properties
                disease.Name = updateDiseaseDTO.Name;
                disease.Description = updateDiseaseDTO.Description;
                disease.Symptoms = updateDiseaseDTO.Symptoms;
                disease.Medication = updateDiseaseDTO.Medication;

                _unitOfWork.diseaseRepository.Update(disease);
                await _unitOfWork.SaveAsync();

                return new ResponseDTO()
                {
                    Message = "Disease updated successfully",
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
