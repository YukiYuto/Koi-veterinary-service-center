using System.Security.Claims;
using AutoMapper;
using FirebaseAdmin.Messaging;
using KoiVeterinaryServiceCenter.DataAccess.IRepository;
using KoiVeterinaryServiceCenter.Model.DTO;
using KoiVeterinaryServiceCenter.Models.Domain;
using KoiVeterinaryServiceCenter.Models.DTO;
using KoiVeterinaryServiceCenter.Models.DTO.DoctorSchedules;
using KoiVeterinaryServiceCenter.Services.IServices;
using Microsoft.IdentityModel.Tokens;

namespace KoiVeterinaryServiceCenter.Services.Services;

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

    public async Task<ResponseDTO> GetDoctorScheduleByDoctorId(ClaimsPrincipal User, Guid doctorId)
    {
        try
        {
            var doctor = await _unitOfWork.DoctorSchedulesRepository.GetAsync(d => d.DoctorId == doctorId);
            if (doctor == null)
            {
                return new ResponseDTO()
                {
                    Message = "Doctor not found",
                    IsSuccess = false,
                    StatusCode = 404,
                    Result = null
                };
            }

            GetDoctorSchedulesIdDTO doctorSchedulesIdDTO;
            doctorSchedulesIdDTO = _mapper.Map<GetDoctorSchedulesIdDTO>(doctor);

            return new ResponseDTO()
            {
                Message = "Get doctor schedule successfully",
                IsSuccess = true,
                StatusCode = 200,
                Result = doctorSchedulesIdDTO
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
    //Update doctor schedule
    public async Task<ResponseDTO> UpdateDoctorScheduleById(ClaimsPrincipal User,
        UpdateDoctorSchedulesDTO updateDoctorSchedulesDTO)
    {
        try
        {
            //Use ID to find the doctor schedule
            var updateDoctorSchedules = await _unitOfWork.DoctorSchedulesRepository.GetAsync(x =>
                x.DoctorSchedulesId == updateDoctorSchedulesDTO.DoctorSchedulesId);

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
            var doctorScheduleToDelete =
                await _unitOfWork.DoctorSchedulesRepository.GetAsync(x => x.DoctorSchedulesId == doctorScheduleId);

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
    public async Task<ResponseDTO> GetAll
    (
        ClaimsPrincipal User,
        string? filterOn,
        string? filterQuery,
        string? sortBy,
        bool? isAscending,
        int pageNumber = 0,
        int pageSize = 0
    )
    {
        try
        {
            // Lấy tất cả lịch trình của bác sĩ
            var allDoctorSchedules =
                (await _unitOfWork.DoctorSchedulesRepository.GetAllAsync(includeProperties: "Doctor.ApplicationUser"));

            // Khởi tạo danh sách doctorSchedules
            List<DoctorSchedules> doctorSchedules = allDoctorSchedules.ToList();

            // Filter Query
            if (!string.IsNullOrEmpty(filterOn) && !string.IsNullOrEmpty(filterQuery))
            {
                doctorSchedules = doctorSchedules.Where(x =>
                {
                    switch (filterOn.Trim().ToLower())
                    {
                        case "doctorname":
                            return x.Doctor.ApplicationUser.FullName.Contains(filterQuery,
                                StringComparison.CurrentCultureIgnoreCase);

                        case "createdby":
                            return x.CreatedBy.Contains(filterQuery, StringComparison.CurrentCultureIgnoreCase);

                        case "updateby":
                            return x.UpdatedBy.Contains(filterQuery, StringComparison.CurrentCultureIgnoreCase);

                        default:
                            return true; // Trả về tất cả nếu không có điều kiện khớp
                    }
                }).ToList();
            }

            // Sắp xếp nếu có yêu cầu
            if (!string.IsNullOrEmpty(sortBy))
            {
                sortBy = sortBy.Trim().ToLower();
                switch (sortBy)
                {
                    case "doctorname":
                        doctorSchedules = doctorSchedules.OrderBy(x => x.Doctor.ApplicationUser.FullName).ToList();
                        break;

                    case "createdby":
                        doctorSchedules = doctorSchedules.OrderBy(x => x.CreatedBy).ToList();
                        break;

                    case "updateby":
                        doctorSchedules = doctorSchedules.OrderBy(x => x.UpdatedBy).ToList();
                        break;

                    default:
                        break; // Nếu không có trường hợp nào khớp, giữ nguyên danh sách
                }
            }

            // Phân trang
            if (pageNumber > 0 && pageSize > 0)
            {
                var skipResult = (pageNumber - 1) * pageSize;
                doctorSchedules = doctorSchedules.Skip(skipResult).Take(pageSize).ToList();
            }

            // Kiểm tra xem danh sách có trống không
            if (doctorSchedules == null || !doctorSchedules.Any())
            {
                return new ResponseDTO()
                {
                    Message = "No doctor schedules found",
                    Result = null,
                    IsSuccess = false,
                    StatusCode = 404
                };
            }

            // Chuyển đổi sang DTO (nếu cần)
            List<GetDoctorSchedulesDTO> getDoctorSchedulesDTO =
                _mapper.Map<List<GetDoctorSchedulesDTO>>(doctorSchedules);

            return new ResponseDTO()
            {
                Message = "Doctor schedules retrieved successfully",
                Result = getDoctorSchedulesDTO,
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