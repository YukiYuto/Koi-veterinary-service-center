using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using KoiVeterinaryServiceCenter.DataAccess.IRepository;
using KoiVeterinaryServiceCenter.Models.Domain;
using KoiVeterinaryServiceCenter.Models.DTO;
using KoiVeterinaryServiceCenter.Services.IServices;

namespace KoiVeterinaryServiceCenter.Services.Services
{
    public class PetDiseaseService : IPetDiseaseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PetDiseaseService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseDTO> CreatePetDisease(ClaimsPrincipal user, CreatePetDiseaseDTO petDiseaseDTO)
        {
            try
            {
                PetDisease petDisease = new PetDisease()
                {
                    PetId = petDiseaseDTO.PetId,
                    DiseaseId = petDiseaseDTO.DiseaseId,
                    Status = 0,
                    CreatedTime = DateTime.Now,
                    CreatedBy = user.Identity?.Name
                };

                await _unitOfWork.PetDiseaseRepository.AddAsync(petDisease);
                await _unitOfWork.SaveAsync();
                return new ResponseDTO
                {
                    Message = "Added pet disease successfully",
                    IsSuccess = true,
                    StatusCode = 200,
                    Result = null
                };
            }
            catch (Exception e)
            {
                return new ResponseDTO
                {
                    Message = e.Message,
                    IsSuccess = false,
                    StatusCode = 500,
                    Result = null
                };
            }
        }

        public async Task<ResponseDTO> GetAllPetDiseases(ClaimsPrincipal user, string? filterOn, string? filterQuery, string? sortBy, bool? isAscending, int pageNumber, int pageSize)
        {
            try
            {
                var petDiseases = await _unitOfWork.PetDiseaseRepository.GetAllAsync();

                // Optional: Implement filtering and sorting logic here based on parameters
                // For example, filter on `filterOn` and `filterQuery`, sort on `sortBy`

                var petDiseaseDTOs = _mapper.Map<List<GetPetDiseaseDTO>>(petDiseases);
                return new ResponseDTO
                {
                    Message = "Retrieved all pet diseases successfully",
                    IsSuccess = true,
                    StatusCode = 200,
                    Result = petDiseaseDTOs
                };
            }
            catch (Exception e)
            {
                return new ResponseDTO
                {
                    Message = e.Message,
                    IsSuccess = false,
                    StatusCode = 500,
                    Result = null
                };
            }
        }

        public async Task<ResponseDTO> GetPetDiseasesByPetId(Guid petId)
        {
            try
            {
                var petDiseases = await _unitOfWork.PetDiseaseRepository.GetByPetId(petId);
                if (petDiseases == null || petDiseases.Count == 0)
                {
                    return new ResponseDTO
                    {
                        Message = "No diseases found for this pet",
                        IsSuccess = false,
                        StatusCode = 404,
                        Result = null
                    };
                }

                var petDiseaseDTOs = _mapper.Map<List<GetPetDiseaseDTO>>(petDiseases);
                return new ResponseDTO
                {
                    Message = "Retrieved pet diseases successfully",
                    IsSuccess = true,
                    StatusCode = 200,
                    Result = petDiseaseDTOs
                };
            }
            catch (Exception e)
            {
                return new ResponseDTO
                {
                    Message = e.Message,
                    IsSuccess = false,
                    StatusCode = 500,
                    Result = null
                };
            }
        }

        public async Task<ResponseDTO> GetPetDiseasesByDiseaseId(Guid diseaseId)
        {
            try
            {
                var petDiseases = await _unitOfWork.PetDiseaseRepository.GetByDiseaseId(diseaseId);
                if (petDiseases == null || petDiseases.Count == 0)
                {
                    return new ResponseDTO
                    {
                        Message = "No pets found with this disease",
                        IsSuccess = false,
                        StatusCode = 404,
                        Result = null
                    };
                }

                var petDiseaseDTOs = _mapper.Map<List<GetPetDiseaseDTO>>(petDiseases);
                return new ResponseDTO
                {
                    Message = "Retrieved pets with this disease successfully",
                    IsSuccess = true,
                    StatusCode = 200,
                    Result = petDiseaseDTOs
                };
            }
            catch (Exception e)
            {
                return new ResponseDTO
                {
                    Message = e.Message,
                    IsSuccess = false,
                    StatusCode = 500,
                    Result = null
                };
            }
        }

        //public async Task<ResponseDTO> UpdatePetDisease(ClaimsPrincipal user, UpdatePetDiseaseDTO petDiseaseDTO)
        //{
        //    try
        //    {
        //        var petDisease = await _unitOfWork.PetDiseaseRepository.GetByDiseaseId(petDiseaseDTO.Id);
        //        if (petDisease == null)
        //        {
        //            return new ResponseDTO
        //            {
        //                Message = "Pet disease not found",
        //                IsSuccess = false,
        //                StatusCode = 404,
        //                Result = null
        //            };
        //        }

        //        petDisease.DiseaseId = petDiseaseDTO.DiseaseId; // Update as needed
        //        _unitOfWork.PetDiseaseRepository.Update(petDisease);
        //        await _unitOfWork.SaveAsync();

        //        return new ResponseDTO
        //        {
        //            Message = "Updated pet disease successfully",
        //            IsSuccess = true,
        //            StatusCode = 200,
        //            Result = null
        //        };
        //    }
        //    catch (Exception e)
        //    {
        //        return new ResponseDTO
        //        {
        //            Message = e.Message,
        //            IsSuccess = false,
        //            StatusCode = 500,
        //            Result = null
        //        };
        //    }
        }
    }

