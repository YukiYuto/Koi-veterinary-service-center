using System.Security.Claims;
using AutoMapper;
using KoiVeterinaryServiceCenter.DataAccess.IRepository;
using KoiVeterinaryServiceCenter.Model.Domain;
using KoiVeterinaryServiceCenter.Model.DTO;
using KoiVeterinaryServiceCenter.Models.DTO;
using KoiVeterinaryServiceCenter.Models.DTO.DoctorSchedules;
using KoiVeterinaryServiceCenter.Services.IServices;
using Microsoft.IdentityModel.Tokens;

namespace KoiVeterinaryServiceCenter.Services.Services
{
    public class DoctorSchedulesService : IDoctorSchedulesService
    {
        public readonly IUnitOfWork _unitOfWork;
        public readonly IMapper _mapper;
        public DoctorSchedulesService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        //Create Schedule for doctor
        public async Task<ResponseDTO> CreateDoctorSchedule(ClaimsPrincipal User,
        CreateDoctorSchedulesDTO createDoctorSchedulesDTO)
        {
            try
            {
                //Map DTO to entity
                DoctorSchedules doctorSchedules = new DoctorSchedules()
                {
                    DoctorId = createDoctorSchedulesDTO.DoctorId,
                    SchedulesDate = createDoctorSchedulesDTO.SchedulesDate,
                    CreatedBy = User.Identity.Name,
                    CreatedTime = DateTime.Now,
                    Status = 0
                };

                //Add new schedule for doctor
                await _unitOfWork.DoctorSchedulesRepository.AddAsync(doctorSchedules);
                await _unitOfWork.SaveAsync();

                //If function successfuly, this will create object which is DoctorSchedules into database and message successfully
                return new ResponseDTO()
                {
                    Message = "Create schedule for the doctor successfully",
                    IsSuccess = true,
                    StatusCode = 200,
                    Result = doctorSchedules
                };
            }
            //Solve all exception
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

        //Get doctor schedule by ID
        public async Task<ResponseDTO> GetDoctorScheduleById(ClaimsPrincipal User, Guid doctorSchedulesId)
        {
            try
            {
                //Get DoctorSchedules by doctorSchedulesId
                var doctorSchedule = await _unitOfWork.DoctorSchedulesRepository.GetDocterScheduleById(doctorSchedulesId);
                //Solve doctorSchedules is null
                if (doctorSchedule == null)
                {
                    return new ResponseDTO()
                    {
                        Message = "Doctor schedule was not exist",
                        IsSuccess = false,
                        StatusCode = 404,
                        Result = null
                    };
                }
                //Use mapping from DoctorSchedules to GetDoctorSchedulesDTO
                try
                {
                    GetDoctorSchedulesDTO doctorSchedulesDTO;
                    doctorSchedulesDTO = _mapper.Map<GetDoctorSchedulesDTO>(doctorSchedule);

                }
                //Solve the mapping exception
                catch (AutoMapperMappingException e)
                {
                    return new ResponseDTO()
                    {
                        Message = "Failed to map DoctorSchedules to GetDoctorSchedulesDTO",
                        IsSuccess = false,
                        StatusCode = 500,
                        Result = null
                    };
                }
                //If function successfuly, they will return object which is DoctorSchedules and message successfully
                return new ResponseDTO()
                {
                    Message = "Get doctor schedule successfully",
                    IsSuccess = true,
                    StatusCode = 200,
                    Result = doctorSchedule
                };
            }
            //Solve all exception
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

        //Update doctor schedule
        public async Task<ResponseDTO> UpdateDoctorScheduleById(ClaimsPrincipal User, UpdateDoctorSchedulesDTO updateDoctorSchedulesDTO)
        {
            try
            {
                //Use ID to find the doctor schedule
                var updateDoctorSchedules = await _unitOfWork.DoctorSchedulesRepository.GetAsync(x => x.DoctorSchedulesId == updateDoctorSchedulesDTO.DoctorSchedulesId);

                //check doctor schedule is exist or not if this not exist return not exist form
                if (updateDoctorSchedules is null)
                {
                    return new ResponseDTO()
                    {
                        Message = "Doctor schedule not exist",
                        IsSuccess = false,
                        StatusCode = 404,
                        Result = null
                    };
                }

                //Update data of doctor schedule
                updateDoctorSchedules.DoctorId = updateDoctorSchedules.DoctorId;
                updateDoctorSchedules.SchedulesDate = updateDoctorSchedulesDTO.SchedulesDate;
                updateDoctorSchedules.UpdatedTime = DateTime.Now;
                updateDoctorSchedules.UpdatedBy = User.Identity.Name;

                //Update to database and save it
                _unitOfWork.DoctorSchedulesRepository.Update(updateDoctorSchedules);
                await _unitOfWork.SaveAsync();

                //result of success
                return new ResponseDTO()
                {
                    Message = "Update doctor schedule successfully",
                    IsSuccess = true,
                    StatusCode = 200,
                    Result = updateDoctorSchedulesDTO
                };
            }
            //Solve exception when the function has error
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

        //Use doctor schedule ID to find and then delete it by update status is 1
        public async Task<ResponseDTO> DeleteDoctorScheduleById(ClaimsPrincipal User, Guid doctorScheduleId)
        {
            try
            {
                //Find doctor schedule from DataBase
                var doctorScheduleToDelete = await _unitOfWork.DoctorSchedulesRepository.GetAsync(x => x.DoctorSchedulesId == doctorScheduleId);

                //Resolve error if doctor is null
                if (doctorScheduleToDelete is null)
                {
                    return new ResponseDTO()
                    {
                        Message = "Doctor schedule not exsit",
                        IsSuccess = false,
                        StatusCode = 404,
                        Result = null
                    };
                }

                //Update status is 1, this mean doctor schedule was deleted
                doctorScheduleToDelete.Status = 1;
                //Record time when someone who delete doctor schedule
                doctorScheduleToDelete.UpdatedTime = DateTime.Now;
                //Record name when someone who delete doctor schedule
                doctorScheduleToDelete.UpdatedBy = User.Identity.Name;

                //Update to database and save it
                _unitOfWork.DoctorSchedulesRepository.Update(doctorScheduleToDelete);
                await _unitOfWork.SaveAsync();

                //result of a success
                return new ResponseDTO()
                {
                    Message = "The doctor schedule was deleted successfully",
                    IsSuccess = true,
                    StatusCode = 200,
                    Result = null
                };
            }
            //Solve exception when the function has error
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

        //Get all doctor schedules by filter, sort query and pagination
        public async Task<ResponseDTO> GetAll(ClaimsPrincipal User, string? filterOn, string? filterQuery, string? sortBy, bool? isAscending, int pageNumber, int pageSize)
        {
            try
            {
                #region Query Parameters
                //Create a new doctor schedules list
                List<DoctorSchedules> doctorSchedules = new List<DoctorSchedules>();

                //check input filter is null or have a value
                if (!string.IsNullOrEmpty(filterOn) && !string.IsNullOrEmpty(filterQuery))
                {
                    //remove space and lower to word
                    switch (filterOn.Trim().ToLower())
                    {
                        case "doctorname":
                            {
                                //get all doctor schedules list with condition which is doctor name must match filterOn
                                doctorSchedules = _unitOfWork.DoctorSchedulesRepository.GetAllAsync(includeProperties: "Doctor.ApplicationUser")
                                    .GetAwaiter().GetResult().Where(x => x.Doctor.ApplicationUser.FullName.Contains(filterQuery, StringComparison.CurrentCultureIgnoreCase)).ToList();
                                break;
                            }
                        //Just for admin
                        case "createdby":
                            {
                                //get all doctor schedules list with condition which is created by must match filterOn
                                doctorSchedules = _unitOfWork.DoctorSchedulesRepository.GetAllAsync(includeProperties: "Doctor.ApplicationUser")
                                    .GetAwaiter().GetResult().Where(x => x.CreatedBy.Contains(filterQuery, StringComparison.CurrentCultureIgnoreCase)).ToList();
                                break;
                            }
                        //Just for admin
                        case "updateby":
                            {
                                //get all doctor schedules list with condition which is update by must match filterOn
                                doctorSchedules = _unitOfWork.DoctorSchedulesRepository.GetAllAsync(includeProperties: "Doctor.ApplicationUser")
                                    .GetAwaiter().GetResult().Where(x => x.UpdatedBy.Contains(filterQuery, StringComparison.CurrentCultureIgnoreCase)).ToList();
                                break;
                            }
                        default:
                            //get all doctor schedules list without condition
                            doctorSchedules = _unitOfWork.DoctorSchedulesRepository.GetAllAsync(includeProperties: "Doctor.ApplicationUser")
                                    .GetAwaiter().GetResult().ToList();
                            break;
                    }
                }
                else
                {
                    //get all doctor schedules list without condition
                    doctorSchedules = _unitOfWork.DoctorSchedulesRepository.GetAllAsync(includeProperties: "Doctor.ApplicationUser")
                            .GetAwaiter().GetResult().ToList();
                }

                //check sortBy is null or have a value
                if (!string.IsNullOrEmpty(sortBy))
                {
                    //remove space and lower to word
                    switch (sortBy.Trim().ToLower())
                    {
                        //sort doctor schedules list acsending by name
                        case "name":
                            {
                                doctorSchedules = isAscending == true
                                    ? [.. doctorSchedules.OrderBy(name => name.Doctor.ApplicationUser.FullName)]
                                    : [.. doctorSchedules.OrderByDescending(name => name.Doctor.ApplicationUser.FullName)];
                                break;
                            }
                        //defulat do not anything
                        default:
                            break;
                    }
                }

                //Pagination
                //Check page number and page size have to bigger than 0
                if (pageNumber > 0 && pageSize > 0)
                {
                    //this is a number which is list will skip
                    var skipResult = (pageNumber - 1) * pageSize;
                    //list will skip with number above and take with number by page size
                    doctorSchedules = doctorSchedules.Skip(skipResult).Take(pageSize).ToList();
                }
                #endregion Query Parameters
                //Solve doctor schedules list is null or empty
                if (doctorSchedules.IsNullOrEmpty())
                {
                    return new ResponseDTO()
                    {
                        Message = "Doctor schedule is not exsit",
                        IsSuccess = false,
                        StatusCode = 404,
                        Result = null
                    };
                }
                //Create a new list with object is GetDoctorSchedulesDTO
                List<GetDoctorSchedulesDTO> getDoctorSchedulesDTO = new List<GetDoctorSchedulesDTO>();
                try
                {
                    //Mapping the list doctorSchedules to GetDoctorSchedulesDTO
                    getDoctorSchedulesDTO = _mapper.Map<List<GetDoctorSchedulesDTO>>(doctorSchedules);
                }
                //Solve auto mapping exception
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
                //If function successfuly return doctor schedules list
                return new ResponseDTO()
                {
                    Message = "Get doctor schedules list successfully",
                    IsSuccess = true,
                    StatusCode = 200,
                    Result = getDoctorSchedulesDTO
                };
            }
            //Solve all exception
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
