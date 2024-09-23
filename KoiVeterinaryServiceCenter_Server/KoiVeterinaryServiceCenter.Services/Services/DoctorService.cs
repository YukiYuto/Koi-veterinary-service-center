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

        public Task<ResponseDTO> DeleteAsync(String id)
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

                DoctorInfoDTO doctorInfoDto = new DoctorInfoDTO()
                {
                    UserId = doctor.UserId,
                    Specialization = doctor.Specialization,
                    Experience = doctor.Experience,
                    Degree = doctor.Degree
                };

                return new ResponseDTO()
                {
                    Message = "Get doctor successfully",
                    IsSuccess = false,
                    StatusCode = 200,
                    Result = doctorInfoDto
                };
            }
            catch(Exception e)
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

        public Task<ResponseDTO> UpdateDoctorAsync(UpdateDoctorDTO updateDoctorDTO)
        {
            throw new NotImplementedException();
        }
    }