using System;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using KoiVeterinaryServiceCenter.DataAccess.IRepository;
using KoiVeterinaryServiceCenter.Models.Domain;
using KoiVeterinaryServiceCenter.Models.DTO;
using KoiVeterinaryServiceCenter.Models.DTO.PetService;
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

        public async Task<ResponseDTO> AddPetDisease(ClaimsPrincipal user, CreatePetDiseaseDTO petDiseaseDTO)
        {
            try
            {
                var petDisease = _mapper.Map<PetDisease>(petDiseaseDTO);
                await _unitOfWork.PetDiseaseRepository.AddAsync(petDisease);
                await _unitOfWork.SaveAsync();

                return new ResponseDTO
                {
                    Message = "Pet disease added successfully",
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

        public Task<ResponseDTO> GetAllPetDiseases(ClaimsPrincipal user, string? filterOn, string? filterQuery, string? sortBy, bool? isAscending, int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseDTO> GetPetDiseasesByDiseaseId(Guid diseaseId)
        {
            try
            {
                var petDiseases = await _unitOfWork.PetDiseaseRepository.GetByDiseaseId(diseaseId);
                var petDiseaseDTOs = _mapper.Map<IEnumerable<GetPetDiseaseDTO>>(petDiseases);

                return new ResponseDTO
                {
                    Message = "Pet diseases retrieved successfully",
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
                var petDiseaseDTOs = _mapper.Map<IEnumerable<GetPetDiseaseDTO>>(petDiseases);

                return new ResponseDTO
                {
                    Message = "Pet diseases retrieved successfully",
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

        public async Task<ResponseDTO> UpdatePetDisease(ClaimsPrincipal user, UpdatePetDiseaseDTO petDiseaseDTO)
        {
            try
            {
                var petDisease = await _unitOfWork.PetDiseaseRepository.GetById(petDiseaseDTO.PetId, petDiseaseDTO.DiseaseId);
                if (petDisease == null)
                {
                    return new ResponseDTO
                    {
                        Message = "Pet disease not found",
                        IsSuccess = false,
                        StatusCode = 404,
                        Result = null
                    };
                }

                _mapper.Map(petDiseaseDTO, petDisease);
                _unitOfWork.PetDiseaseRepository.Update(petDisease);
                await _unitOfWork.SaveAsync();

                return new ResponseDTO
                {
                    Message = "Pet disease updated successfully",
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
    }
}
