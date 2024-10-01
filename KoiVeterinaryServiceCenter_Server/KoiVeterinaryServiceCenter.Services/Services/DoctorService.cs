using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using KoiVeterinaryServiceCenter.DataAccess.IRepository;
using KoiVeterinaryServiceCenter.Model.Domain;
using KoiVeterinaryServiceCenter.Model.DTO;
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

                var doctorInfoDto = _mapper.Map<List<DoctorInfoDTO>>(doctors);
                return new ResponseDTO()
                {
                    Message = "Get all doctors successfully",
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


        //Get doctor by id
        public async Task<ResponseDTO> GetDoctorById(Guid id)
        {
            try
            {
                var doctor = await _unitOfWork.DoctorRepository.GetById(id);
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
                };

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
        public async Task<ResponseDTO> UpdateDoctorById(UpdateDoctorDTO updateDoctorDTO)
        {
            try
            {
                var doctorToUpdate = await _unitOfWork.DoctorRepository.GetById(updateDoctorDTO.DoctorId);
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

                _unitOfWork.DoctorRepository.Update(doctorToUpdate);
                await _unitOfWork.SaveAsync();

                return new ResponseDTO()
                {
                    Message = "Doctor updated successfully",
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
                    Result = null,
                };
            }
        }
    }
}
