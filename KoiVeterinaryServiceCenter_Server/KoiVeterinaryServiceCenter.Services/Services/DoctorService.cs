using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using KoiVeterinaryServiceCenter.DataAccess.IRepository;
using KoiVeterinaryServiceCenter.Model.Domain;
using KoiVeterinaryServiceCenter.Models.Domain;
using KoiVeterinaryServiceCenter.Models.DTO;
using KoiVeterinaryServiceCenter.Models.DTO.Doctor;
using KoiVeterinaryServiceCenter.Models.DTO.Slot;
using KoiVeterinaryServiceCenter.Services.IServices;
using Microsoft.IdentityModel.Tokens;

namespace KoiVeterinaryServiceCenter.Services.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DoctorService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        //Get all doctor then filter query and sort query after that pagination is excuted
        public async Task<ResponseDTO> GetAll(ClaimsPrincipal User, string? filterOn, string? filterQuery, string? sortBy, bool? isAscending, int pageNumber, int pageSize)
        {
            #region Query Parameters
            try
            {
                List<Doctor> doctors = new List<Doctor>();
                //Filter Query
                if (!string.IsNullOrEmpty(filterOn) && !string.IsNullOrEmpty(filterQuery))
                {
                    switch (filterOn.Trim().ToLower())
                    {
                        case "name":
                            {
                                doctors = _unitOfWork.DoctorRepository.GetAllAsync(includeProperties: "ApplicationUser")
                                .GetAwaiter().GetResult().Where(x =>
                                x.ApplicationUser.FullName.Contains(filterQuery, StringComparison.CurrentCultureIgnoreCase)).ToList();
                                break;
                            }
                        case "phoneNumber":
                            {
                                doctors = _unitOfWork.DoctorRepository.GetAllAsync(includeProperties: "ApplicationUser")
                                .GetAwaiter().GetResult().Where(x =>
                                x.ApplicationUser.PhoneNumber.Contains(filterQuery, StringComparison.CurrentCultureIgnoreCase)).ToList();
                                break;
                            }
                        case "email":
                            {
                                doctors = _unitOfWork.DoctorRepository.GetAllAsync(includeProperties: "ApplicationUser")
                                .GetAwaiter().GetResult().Where(x =>
                                x.ApplicationUser.Email.Contains(filterQuery, StringComparison.CurrentCultureIgnoreCase)).ToList();
                                break;
                            }
                        default:
                            {
                                doctors = _unitOfWork.DoctorRepository.GetAllAsync(includeProperties: "ApplicationUser").
                                GetAwaiter().GetResult().ToList();
                                break;
                            }
                    }
                }
                else
                {
                    doctors = _unitOfWork.DoctorRepository.GetAllAsync(includeProperties: "ApplicationUser").
                    GetAwaiter().GetResult().ToList();
                }

                //Sort Query
                if (!string.IsNullOrEmpty(sortBy))
                {
                    switch (sortBy.Trim().ToLower())
                    {
                        case "name":
                            {
                                doctors = isAscending == true
                                ? [.. doctors.OrderBy(x => x.ApplicationUser.FullName)]
                                : [.. doctors.OrderByDescending(x => x.ApplicationUser.FullName)];
                                break;
                            }
                        case "phoneNumber":
                            {
                                doctors = isAscending == true
                                ? [.. doctors.OrderBy(x => x.ApplicationUser.PhoneNumber)]
                                : [.. doctors.OrderByDescending(x => x.ApplicationUser.PhoneNumber)];
                                break;
                            }
                        case "email":
                            {
                                doctors = isAscending == true
                                ? [.. doctors.OrderBy(x => x.ApplicationUser.Email)]
                                : [.. doctors.OrderByDescending(x => x.ApplicationUser.Email)];
                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }
                }

                //Pagination
                if (pageNumber > 0 && pageSize > 0)
                {
                    var skipResult = (pageNumber - 1) * pageSize;
                    doctors = doctors.Skip(skipResult).Take(pageSize).ToList();
                }

                #endregion Query Parameters

                //Check doctor list and solve error if doctor list is null or empty
                if (doctors.IsNullOrEmpty())
                {
                    return new ResponseDTO()
                    {
                        Message = "Doctor does not exist",
                        IsSuccess = false,
                        StatusCode = 404,
                        Result = null

                    };
                }

                //User map to mapping doctor to DoctorInfoDTO
                var doctorInfoDto = _mapper.Map<List<DoctorInfoDTO>>(doctors);

                //Return doctor list and message if the function successfully
                return new ResponseDTO()
                {
                    Message = "Get all doctors successfully",
                    IsSuccess = true,
                    StatusCode = 200,
                    Result = doctorInfoDto
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


        //Get doctor by id
        public async Task<ResponseDTO> GetDoctorById(ClaimsPrincipal User, Guid id)
        {
            try
            {
                //Get doctor from database by GetById
                var doctor = await _unitOfWork.DoctorRepository.GetById(id);

                //Solve doctor is null
                if (doctor is null)
                {
                    return new ResponseDTO()
                    {
                        Message = "Doctor was not found",
                        IsSuccess = false,
                        StatusCode = 404,
                        Result = null
                    };
                }

                //We has doctor, but not eough information that why we create a new DoctorFullInfoDTO
                //which will get information from doctor and user('s doctor) to combine to full information
                DoctorFullInfoDTO doctorInfoDto = new DoctorFullInfoDTO()
                {
                    DoctorId = doctor.DoctorId,
                    UserId = doctor.UserId,
                    FullName = doctor.ApplicationUser.FullName,
                    PhoneNumber = doctor.ApplicationUser.PhoneNumber,
                    Email = doctor.ApplicationUser.Email,
                    Address = doctor.ApplicationUser.Address,
                    AvatarUrl = doctor.ApplicationUser.AvatarUrl,
                    Country = doctor.ApplicationUser.Country,
                    Gender = doctor.ApplicationUser.Gender,
                    BirthDate = doctor.ApplicationUser.BirthDate,
                    Specialization = doctor.Specialization,
                    Experience = doctor.Experience,
                    Degree = doctor.Degree,
                    Position = doctor.Position
                };

                //Solve return (has object doctorInfoDto) if the function successfully
                return new ResponseDTO()
                {
                    Message = "Get doctor successfully",
                    IsSuccess = true,
                    StatusCode = 200,
                    Result = doctorInfoDto
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

        //Use Doctor ID to find and update information's the Doctor
        public async Task<ResponseDTO> UpdateDoctorById(ClaimsPrincipal User, UpdateDoctorDTO updateDoctorDTO)
        {
            try
            {
                //Get object doctor from database by GetById
                var doctorToUpdate = await _unitOfWork.DoctorRepository.GetById(updateDoctorDTO.DoctorId);

                //Solve doctor is null
                if (doctorToUpdate is null)
                {
                    return new ResponseDTO()
                    {
                        Message = "Doctor was not found",
                        IsSuccess = false,
                        StatusCode = 404,
                        Result = null
                    };
                }

                //Set data update for doctor
                doctorToUpdate.ApplicationUser.FullName = updateDoctorDTO?.FullName;
                doctorToUpdate.ApplicationUser.Gender = updateDoctorDTO?.Gender;
                doctorToUpdate.ApplicationUser.Email = updateDoctorDTO?.Email;
                doctorToUpdate.ApplicationUser.PhoneNumber = updateDoctorDTO?.PhoneNumber;
                doctorToUpdate.ApplicationUser.BirthDate = updateDoctorDTO?.BirthDate;
                doctorToUpdate.ApplicationUser.AvatarUrl = updateDoctorDTO.AvatarUrl;
                doctorToUpdate.ApplicationUser.Country = updateDoctorDTO?.Country;
                doctorToUpdate.ApplicationUser.Address = updateDoctorDTO?.Address;

                doctorToUpdate.Specialization = updateDoctorDTO.Specialization;
                doctorToUpdate.Experience = updateDoctorDTO.Experience;
                doctorToUpdate.Degree = updateDoctorDTO.Degree;
                doctorToUpdate.Position = updateDoctorDTO?.Position;

                //Update to database and save it
                _unitOfWork.DoctorRepository.Update(doctorToUpdate);
                await _unitOfWork.SaveAsync();

                //Solve return if the function successfully
                return new ResponseDTO()
                {
                    Message = "Doctor updated successfully",
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
                    Result = null,
                };
            }
        }


        //Use doctor ID to find then set lockoutEnabled is false (Its mean lockout account's doctor)
        public async Task<ResponseDTO> DeleteDoctorById(ClaimsPrincipal User, Guid doctorId)
        {
            try
            {
                //Get object doctor by GetAsync
                var doctorToDelete = await _unitOfWork.DoctorRepository.GetAsync(x => x.DoctorId == doctorId, includeProperties: "ApplicationUser");

                //Solve doctor is null
                if (doctorToDelete is null)
                {
                    return new ResponseDTO()
                    {
                        Message = "Doctor is not exist",
                        IsSuccess = false,
                        StatusCode = 404,
                        Result = null
                    };
                }

                //Set lockEnabled is false (deleted)
                doctorToDelete.ApplicationUser.LockoutEnabled = false;

                //Update the database and save it
                _unitOfWork.DoctorRepository.Update(doctorToDelete);
                await _unitOfWork.SaveAsync();

                //Solve return if the function successfully
                return new ResponseDTO()
                {
                    Message = "Doctor was deleted",
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

        public async Task<ResponseDTO> CreateGoogleMeetLink(ClaimsPrincipal User, GoogleMeetLinkDTO googleMeetLinkDTO)
        {
            var doctor = await _unitOfWork.DoctorRepository.GetAsync(d => d.DoctorId == googleMeetLinkDTO.doctorId);
            if (doctor is null)
            {
                return new ResponseDTO()
                {
                    IsSuccess = false,
                    StatusCode = 404,
                    Message = "Doctor not found"
                };
            }

            doctor.GoogleMeetLink = googleMeetLinkDTO.GoogleMeetLink;
            _unitOfWork.DoctorRepository.Update(doctor);
            await _unitOfWork.SaveAsync();

            return new ResponseDTO()
            {
                IsSuccess = true,
                StatusCode = 201,
                Result = doctor,
                Message = "Google meet link created successfully"
            };
        }

        public async Task<ResponseDTO> UpdateGoogleMeetLink(ClaimsPrincipal User, GoogleMeetLinkDTO googleMeetLinkDTO)
        {
            var doctor = await _unitOfWork.DoctorRepository.GetAsync(d => d.DoctorId == googleMeetLinkDTO.doctorId);
            if (doctor is null)
            {
                return new ResponseDTO()
                {
                    IsSuccess = false,
                    StatusCode = 404,
                    Message = "Doctor not found"
                };
            }

            doctor.GoogleMeetLink = googleMeetLinkDTO.GoogleMeetLink;
            _unitOfWork.DoctorRepository.Update(doctor);
            await _unitOfWork.SaveAsync();

            return new ResponseDTO()
            {
                IsSuccess = true,
                StatusCode = 200,
                Result = doctor,
                Message = "Google meet link updated successfully"
            };
        }

        public async Task<ResponseDTO> GetAllDoctorBySlot(ClaimsPrincipal User, GetAllDoctorBySlotDTO getDoctorBySlotDTO)
        {
            try
            {

                var serviceId = getDoctorBySlotDTO.ServiceId;
                var slots = _unitOfWork.SlotRepository.GetAllAsync(includeProperties: "DoctorSchedules.Doctor.ApplicationUser,DoctorSchedules.Doctor.DoctorServices")
                    .GetAwaiter().GetResult()
                    .Where(s => s.StartTime >= getDoctorBySlotDTO.StartTime &&
                                s.EndTime <= getDoctorBySlotDTO.EndTime &&
                                s.DoctorSchedules.SchedulesDate == getDoctorBySlotDTO.SchedulesDate &&
                                s.DoctorSchedules.Doctor.DoctorServices.Any(ds => ds.ServiceId == serviceId))
                    .ToList();
                if (slots.IsNullOrEmpty())
                {
                    return new ResponseDTO()
                    {
                        Message = "There are no doctors available in the slot",
                        IsSuccess = false,
                        StatusCode = 404,
                        Result = null
                    };
                }

                List<DoctorFullInfoDTO> doctorList;
                try
                {
                    doctorList = _mapper.Map<List<DoctorFullInfoDTO>>(slots);
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
                    Message = "Get all doctor by slot successfully",
                    IsSuccess = true,
                    StatusCode = 200,
                    Result = doctorList
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
