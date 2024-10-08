using AutoMapper;
using KoiVeterinaryServiceCenter.DataAccess.IRepository;
using KoiVeterinaryServiceCenter.Model.DTO;
using KoiVeterinaryServiceCenter.Model.Domain;
using KoiVeterinaryServiceCenter.Services.IServices;
using System.Security.Claims;


namespace KoiVeterinaryServiceCenter.Services.Services
{
    public class DoctorServicesService : IDoctorServicesService
    {
        public readonly IUnitOfWork _unitOfWork;
        public readonly IMapper _mapper;

        public DoctorServicesService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseDTO> CreateDoctorService(ClaimsPrincipal User, CreateDoctorServicesDTO createDoctorServiceDTO)
        {
            try
            {
                DoctorServices doctorService = new DoctorServices()
                {
                    ServiceId = createDoctorServiceDTO.ServiceId,
                    DoctorId = createDoctorServiceDTO.DoctorId
                };

                await _unitOfWork.DoctorServicesRepository.AddAsync(doctorService);
                await _unitOfWork.SaveAsync();

                return new ResponseDTO()
                {
                    Message = "Create doctor service successfully",
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
        public async Task<ResponseDTO> GetDoctorServiceById(ClaimsPrincipal User, Guid doctorServiceId)
        {
            try
            {
                var doctorService = await _unitOfWork.DoctorServicesRepository.GetById(doctorServiceId);

                if (doctorService is null)
                {
                    return new ResponseDTO()
                    {
                        Message = "Doctor service is not exist",
                        IsSuccess = false,
                        StatusCode = 404,
                        Result = null
                    };
                }

                GetDoctorServicesDTO getDoctorServiceDTO;
                try
                {
                    getDoctorServiceDTO = _mapper.Map<GetDoctorServicesDTO>(doctorService);
                }

                catch (AutoMapperMappingException e)
                {
                    return new ResponseDTO()
                    {
                        Message = e.Message,
                        IsSuccess = false,
                        StatusCode = 500,
                        Result = null
                    };
                }

                return new ResponseDTO()
                {
                    Message = "Get doctor service successfully",
                    IsSuccess = true,
                    StatusCode = 200,
                    Result = getDoctorServiceDTO
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



        public async Task<ResponseDTO> UpdateDoctorService(ClaimsPrincipal User, UpdateDoctorServicesDTO updateDoctorServiceDTO)
        {
            try
            {
                var doctorServiceToUpdate = await _unitOfWork.DoctorServicesRepository.GetById(updateDoctorServiceDTO.DoctorServiceId);
                if (doctorServiceToUpdate is null)
                {
                    return new ResponseDTO()
                    {
                        Message = "Doctor service is not exist",
                        IsSuccess = false,
                        StatusCode = 404,
                        Result = null
                    };
                }
                var doctor = await _unitOfWork.DoctorServicesRepository.GetAsync(x => x.Doctor.DoctorId == updateDoctorServiceDTO.DoctorId);
                if (doctor is null)
                {
                    return new ResponseDTO()
                    {
                        Message = "Doctor is not exist",
                        IsSuccess = false,
                        StatusCode = 404,
                        Result = null
                    };
                }

                doctorServiceToUpdate.DoctorId = updateDoctorServiceDTO.DoctorId;

                _unitOfWork.DoctorServicesRepository.Update(doctorServiceToUpdate);
                await _unitOfWork.SaveAsync();

                return new ResponseDTO()
                {
                    Message = "Update doctor service successfully",
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

        public async Task<ResponseDTO> DeleteDoctorService(ClaimsPrincipal User, Guid doctorServiceId)
        {
            try
            {
                var doctorServiceToDelete = await _unitOfWork.DoctorServicesRepository.GetById(doctorServiceId);
                if (doctorServiceToDelete is null)
                {
                    return new ResponseDTO()
                    {
                        Message = "Doctor service is not exist",
                        IsSuccess = false,
                        StatusCode = 404,
                        Result = null
                    };
                }

                _unitOfWork.DoctorServicesRepository.Remove(doctorServiceToDelete);
                await _unitOfWork.SaveAsync();

                return new ResponseDTO()
                {
                    Message = "Delete doctor service successfully",
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
