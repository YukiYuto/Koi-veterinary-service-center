using System.Security.Claims;
using AutoMapper;
using KoiVeterinaryServiceCenter.DataAccess.IRepository;
using KoiVeterinaryServiceCenter.Model.Domain;
using KoiVeterinaryServiceCenter.Model.DTO;
using KoiVeterinaryServiceCenter.Services.IServices;

namespace KoiVeterinaryServiceCenter.Services.Services
{
    public class ServiceService : IServiceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ServiceService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        //The function create new service then save into database
        public async Task<ResponseDTO> CreateService(ClaimsPrincipal User, CreateServiceDTO createServiceDTO)
        {
            try
            {
                //Create new service from createServiceDTO
                Service service = new Service()
                {
                    ServiceName = createServiceDTO.ServiceName,
                    Price = createServiceDTO.Price,
                    TreavelFree = createServiceDTO.TravelFee,
                    CreatedBy = User.Identity.Name,
                    CreatedTime = DateTime.Now,
                    Status = 0
                };

                //Add service to databases and save it
                await _unitOfWork.ServiceRepository.AddAsync(service);
                await _unitOfWork.SaveAsync();

                //Solve return if the function successfully
                return new ResponseDTO()
                {
                    Message = "Create service successfully",
                    IsSuccess = true,
                    StatusCode = 200,
                    Result = null
                };
            }

            //Solve exception if the function has any error
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

        //Get service by ID
        public async Task<ResponseDTO> GetServiceById(ClaimsPrincipal User, Guid serviceId)
        {
            try
            {
                //Get service from database
                var service = await _unitOfWork.ServiceRepository.GetServiceById(serviceId);

                //Solve service is null
                if (service is null)
                {
                    return new ResponseDTO()
                    {
                        Message = "Service is not exist",
                        IsSuccess = false,
                        StatusCode = 404,
                        Result = null
                    };
                }

                //Use Map to mapping service to GetServiceDTO
                GetServiceDTO getService;
                try
                {
                    getService = _mapper.Map<GetServiceDTO>(service);
                }
                //Solve exception if auto mapper has error
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

                //Solve return if the function successfully
                return new ResponseDTO()
                {
                    Message = "Get service successfully",
                    IsSuccess = true,
                    StatusCode = 200,
                    Result = getService
                };
            }

            //Solve exception if the function has any error
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

        //Use service ID to find the service then update it with data form
        public async Task<ResponseDTO> UpdateService(ClaimsPrincipal User, UpdateServiceDTO updateServiceDTO)
        {
            try
            {
                //Get service need to update from database
                var serviceToUpdate = await _unitOfWork.ServiceRepository.GetAsync(x => x.ServiceId == updateServiceDTO.ServiceId);

                //Solve service is null
                if (serviceToUpdate is null)
                {
                    return new ResponseDTO()
                    {
                        Message = "Service is not exist",
                        IsSuccess = false,
                        StatusCode = 404,
                        Result = null
                    };
                }

                //Update service from updateServiceDTO
                serviceToUpdate.ServiceName = updateServiceDTO.ServiceName;
                serviceToUpdate.Price = updateServiceDTO.Price;
                serviceToUpdate.TreavelFree = updateServiceDTO.TravelFee;
                serviceToUpdate.UpdatedBy = User.Identity.Name;
                serviceToUpdate.UpdatedTime = DateTime.Now;

                //User ServiceRepository to update and save it
                _unitOfWork.ServiceRepository.Update(serviceToUpdate);
                await _unitOfWork.SaveAsync();

                //Solve return if the function successfuly
                return new ResponseDTO()
                {
                    Message = "Update service successfully",
                    IsSuccess = true,
                    StatusCode = 200,
                    Result = null
                };
            }

            //Solve exception if the function has any error
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

        //Use service ID to find and update staus is 1 (It's mean service was deleted)
        public async Task<ResponseDTO> DeleteService(ClaimsPrincipal User, Guid serviceId)
        {
            try
            {
                //Get service from database
                var serviceToDelete = await _unitOfWork.ServiceRepository.GetServiceById(serviceId);

                //Solve service is null
                if (serviceToDelete is null)
                {
                    return new ResponseDTO()
                    {
                        Message = "Service is not exsit",
                        IsSuccess = false,
                        StatusCode = 404,
                        Result = null
                    };
                }

                //Update service' status is 1
                //Record time when someone deleted it
                //Record name when someone deleted it
                serviceToDelete.Status = 1;
                serviceToDelete.UpdatedBy = User.Identity.Name;
                serviceToDelete.UpdatedTime = DateTime.Now;

                //Use ServiceRepository to update service into database and save it
                _unitOfWork.ServiceRepository.Update(serviceToDelete);
                await _unitOfWork.SaveAsync();

                //Solve return if the function successfully
                return new ResponseDTO()
                {
                    Message = "Delete service successfully",
                    IsSuccess = true,
                    StatusCode = 200,
                    Result = null
                };
            }

            //Solve exception if the function has error
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
