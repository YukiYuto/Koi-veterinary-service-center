using KoiVeterinaryServiceCenter.DataAccess.IRepository;
using KoiVeterinaryServiceCenter.Model.Domain;
using KoiVeterinaryServiceCenter.Model.DTO;
using KoiVeterinaryServiceCenter.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public async Task<ResponseDTO> CreateDisease(CreateDiseaseDTO createDiseaseDto)
        {
            try
            {
                var disease = new Disease
                {
                    DiseaseName = createDiseaseDto.DiseaseName,
                    DiseaseDescription = createDiseaseDto.DiseaseDescription,
                    DiseaseSymptoms = createDiseaseDto.DiseaseSymptoms,
                    DiseaseTreatment = createDiseaseDto.DiseaseTreatment
                };

                // Add new Disease
                await _unitOfWork.DiseaseRepository.AddAsync(disease);
                await _unitOfWork.SaveAsync();

                return new ResponseDTO
                {
                    Message = "Disease created successfully",
                    Result = disease,
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

        public async Task<ResponseDTO> UpdateDisease(Guid diseaseId, UpdateDiseaseDTO updateDiseaseDto)
        {
            try
            {
                // Retrieve the existing disease using the repository
                var existingDisease = await _unitOfWork.DiseaseRepository.GetAsync(d => d.DiseaseId == diseaseId);

                if (existingDisease == null)
                {
                    return new ResponseDTO
                    {
                        Result = null,
                        Message = "Disease not found",
                        IsSuccess = false,
                        StatusCode = 404
                    };
                }

                // Update properties of the existing disease
                existingDisease.DiseaseName = updateDiseaseDto.DiseaseName ?? existingDisease.DiseaseName;
                existingDisease.DiseaseDescription = updateDiseaseDto.DiseaseDescription ?? existingDisease.DiseaseDescription;
                existingDisease.DiseaseSymptoms = updateDiseaseDto.DiseaseSymptoms ?? existingDisease.DiseaseSymptoms;
                existingDisease.DiseaseTreatment = updateDiseaseDto.DiseaseTreatment ?? existingDisease.DiseaseTreatment;

                // Save changes to the database
                _unitOfWork.DiseaseRepository.Update(existingDisease);
                await _unitOfWork.SaveAsync();

                return new ResponseDTO
                {
                    Result = existingDisease,
                    Message = "Disease updated successfully",
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

        public async Task<ResponseDTO> GetDisease(Guid diseaseId)
        {
            try
            {
                var disease = await _unitOfWork.DiseaseRepository.GetAsync(d => d.DiseaseId == diseaseId);

                if (disease == null)
                {
                    return new ResponseDTO
                    {
                        Result = null,
                        Message = "Disease was not found",
                        IsSuccess = false,
                        StatusCode = 404
                    };
                }

                var diseaseDto = new GetDiseaseDTO
                {
                    DiseaseId = disease.DiseaseId,
                    DiseaseName = disease.DiseaseName,
                    DiseaseDescription = disease.DiseaseDescription,
                    DiseaseSymptoms = disease.DiseaseSymptoms,
                    DiseaseTreatment = disease.DiseaseTreatment
                };

                return new ResponseDTO
                {
                    Result = diseaseDto,
                    Message = "Get disease successfully",
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

        // Assuming the DeleteDisease method is needed, here's a simple implementation
        public async Task<ResponseDTO> DeleteDisease(Guid diseaseId)
        {
            try
            {
                var disease = await _unitOfWork.DiseaseRepository.GetAsync(d => d.DiseaseId == diseaseId);
                if (disease == null)
                {
                    return new ResponseDTO
                    {
                        Message = "Disease not found",
                        IsSuccess = false,
                        StatusCode = 404
                    };
                }

                _unitOfWork.DiseaseRepository.Remove(disease);
                await _unitOfWork.SaveAsync();

                return new ResponseDTO
                {
                    Message = "Disease deleted successfully",
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
    }
}

