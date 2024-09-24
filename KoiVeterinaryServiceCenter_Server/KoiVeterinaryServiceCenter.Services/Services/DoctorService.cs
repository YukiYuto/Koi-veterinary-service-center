using AutoMapper;
using KoiVeterinaryServiceCenter.DataAccess.IRepository;
using KoiVeterinaryServiceCenter.DataAccess.Repository;
using KoiVeterinaryServiceCenter.Model.DTO;
using KoiVeterinaryServiceCenter.Services.IServices;

namespace KoiVeterinaryServiceCenter.Services.Services;
public class DoctorService : IDoctorService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public DoctorService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public Task<ResponseDTO> DeleteDoctorById(String id)
    {
        throw new NotImplementedException();
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