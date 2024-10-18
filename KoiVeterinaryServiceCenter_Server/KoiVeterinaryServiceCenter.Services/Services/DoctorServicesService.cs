using AutoMapper;
using KoiVeterinaryServiceCenter.DataAccess.IRepository;
using KoiVeterinaryServiceCenter.Model.DTO;
using KoiVeterinaryServiceCenter.Model.Domain;
using KoiVeterinaryServiceCenter.Services.IServices;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;


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

        public async Task<ResponseDTO> GetAll(ClaimsPrincipal User, string? filterOn, string? filterQuery, string? sortBy, bool? isAscending, int pageNumber, int pageSize)
        {
            try
            {
                // Initialize an empty list to store the result of DoctorServices query.
                List<DoctorServices> doctorServices = new List<DoctorServices>();

                // Check if there is a filter provided (filterOn) and a query string (filterQuery).
                if (!string.IsNullOrEmpty(filterOn) && !string.IsNullOrEmpty(filterQuery))
                {
                    // Depending on the filter, we can filter by service name (ServiceName) or doctor name (DoctorName).
                    switch (filterOn.Trim().ToLower())
                    {
                        case "servicename":
                            {
                                // Filter by service name, including Doctor and Service in the query.
                                doctorServices = _unitOfWork.DoctorServicesRepository.GetAllAsync(includeProperties: "Doctor.ApplicationUser,Service")
                                    .GetAwaiter().GetResult().Where(ds => ds.Service.ServiceName
                                    .Contains(filterQuery, StringComparison.CurrentCultureIgnoreCase)).ToList();
                                break;
                            }
                        case "doctorname":
                            {
                                // Filter by doctor's name, including Doctor and ApplicationUser in the query.
                                doctorServices = _unitOfWork.DoctorServicesRepository.GetAllAsync(includeProperties: "Doctor.ApplicationUser,Service")
                                    .GetAwaiter().GetResult().Where(ds => ds.Doctor.ApplicationUser.FullName
                                    .Contains(filterQuery, StringComparison.CurrentCultureIgnoreCase)).ToList();
                                break;
                            }
                        default:
                            // If no specific filter is provided, fetch all doctor services with Doctor and Service details.
                            doctorServices = _unitOfWork.DoctorServicesRepository.GetAllAsync(includeProperties: "Doctor.ApplicationUser,Service")
                                .GetAwaiter().GetResult().ToList();
                            break;
                    }
                }
                else
                {
                    // If no filter is provided, fetch all doctor services with Doctor and Service details.
                    doctorServices = _unitOfWork.DoctorServicesRepository.GetAllAsync(includeProperties: "Doctor.ApplicationUser,Service")
                                .GetAwaiter().GetResult().ToList();
                }

                // Sorting the result based on sortBy field (price, travel fee) and order direction (ascending or descending).
                if (!string.IsNullOrEmpty(sortBy))
                {
                    switch (sortBy.Trim().ToLower())
                    {
                        case "price":
                            {
                                // Sort by price, either in ascending or descending order.
                                doctorServices = isAscending == true
                                    ? doctorServices.OrderBy(ds => ds.Service.Price).ToList()
                                    : doctorServices.OrderByDescending(ds => ds.Service.Price).ToList();
                                break;
                            }
                        case "travelfee":
                            {
                                // Sort by travel fee, either in ascending or descending order.
                                doctorServices = isAscending == true
                                    ? doctorServices.OrderBy(ds => ds.Service.TreavelFree).ToList()
                                    : doctorServices.OrderByDescending(ds => ds.Service.TreavelFree).ToList();
                                break;
                            }
                        default:
                            // No sorting if sortBy is not recognized.
                            break;
                    }
                }

                // Apply pagination if pageNumber and pageSize are provided.
                if (pageNumber > 0 && pageSize > 0)
                {
                    // Skip the previous results based on page number and take the next page size.
                    var skipResult = (pageNumber - 1) * pageSize;
                    doctorServices = doctorServices.Skip(skipResult).Take(pageSize).ToList();
                }

                // If no doctor services were found, return a response indicating the service does not exist.
                if (doctorServices.IsNullOrEmpty())
                {
                    return new ResponseDTO()
                    {
                        Message = "Doctor service is not exist",
                        IsSuccess = false,
                        StatusCode = 404,
                        Result = null
                    };
                }

                // Map the doctorServices to DTO (Data Transfer Object) to return to the user.
                List<GetDoctorServicesDTO> getDoctorServicesDTO = new List<GetDoctorServicesDTO>();
                try
                {
                    // Map the DoctorServices list to GetDoctorServicesDTO list using AutoMapper.
                    getDoctorServicesDTO = _mapper.Map<List<GetDoctorServicesDTO>>(doctorServices);
                }
                catch (AutoMapperMappingException e)
                {
                    // Return error if there is a mapping exception.
                    return new ResponseDTO()
                    {
                        Message = e.Message,
                        IsSuccess = false,
                        StatusCode = 500,
                        Result = null
                    };
                }

                // Return the successful response with the mapped data.
                return new ResponseDTO()
                {
                    Message = "Get all doctor service successfully",
                    IsSuccess = true,
                    StatusCode = 200,
                    Result = getDoctorServicesDTO
                };
            }
            catch (Exception e)
            {
                // Catch any other exceptions and return an error response.
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
