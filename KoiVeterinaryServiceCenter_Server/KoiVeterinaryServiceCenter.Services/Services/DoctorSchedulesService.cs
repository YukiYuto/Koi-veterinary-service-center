using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using KoiVeterinaryServiceCenter.DataAccess.IRepository;
using KoiVeterinaryServiceCenter.DataAccess.Repository;
using KoiVeterinaryServiceCenter.Model.Domain;
using KoiVeterinaryServiceCenter.Model.DTO;
using KoiVeterinaryServiceCenter.Services.IServices;

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
        public async Task<ResponseDTO> CreateDoctorSchedule(ClaimsPrincipal User, CreateDoctorSchedulesDTO createDoctorSchedulesDTO)
        {
            try
            {
                //Map DTO to entity
                DoctorSchedules doctorSchedules = new DoctorSchedules()
                {
                    DoctorId = createDoctorSchedulesDTO.DoctorId,
                    SchedulesDate = createDoctorSchedulesDTO.SchedulesDate,
                    StartTime = createDoctorSchedulesDTO.StartTime,
                    EndTime = createDoctorSchedulesDTO.EndTime
                };

                //Add new schedule for doctor
                await _unitOfWork.DoctorSchedulesRepository.AddAsync(doctorSchedules);
                await _unitOfWork.SaveAsync();

                //If function successfuly, they will create object which is DoctorSchedules into database and message successfully
                return new ResponseDTO()
                {
                    Message = "Create schedule for the doctor successfully",
                    IsSuccess = true,
                    StatusCode = 200,
                    Result = doctorSchedules
                };
            }
            //Resolve all exception
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
                //Resolve doctorSchedules is null
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
                //Resolve the mapping exception
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
            //Resolve all exception
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
                updateDoctorSchedules.DoctorId = updateDoctorSchedules.DoctorId;
                updateDoctorSchedules.SchedulesDate = updateDoctorSchedulesDTO.SchedulesDate;
                updateDoctorSchedules.StartTime = updateDoctorSchedulesDTO.StartTime;
                updateDoctorSchedules.EndTime = updateDoctorSchedulesDTO.EndTime;
                updateDoctorSchedules.UpdatedTime = DateTime.Now;
                updateDoctorSchedules.UpdatedBy = User.Identity.Name;

                return new ResponseDTO()
                {
                    Message = "Update doctor schedule successful",
                    IsSuccess = true,
                    StatusCode = 200,
                    Result = updateDoctorSchedulesDTO
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
