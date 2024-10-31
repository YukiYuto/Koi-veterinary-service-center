using System.Security.Claims;
using AutoMapper;
using KoiVeterinaryServiceCenter.DataAccess.IRepository;
using KoiVeterinaryServiceCenter.Models.Domain;
using KoiVeterinaryServiceCenter.Models.DTO;
using KoiVeterinaryServiceCenter.Models.DTO.AppointmentDeposit;
using KoiVeterinaryServiceCenter.Services.IServices;

namespace KoiVeterinaryServiceCenter.Services.Services;

public class AppointmentDepositService : IAppointmentDepositService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AppointmentDepositService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public Task<ResponseDTO> GetAppointmentDeposits
    (ClaimsPrincipal User,
        string? filterOn,
        string? filterQuery,
        string? sortBy,
        int pageNumber = 0,
        int pageSize = 0
    )
    {
        throw new NotImplementedException();
    }

    public Task<ResponseDTO> GetAppointmentDepositById(ClaimsPrincipal User, Guid appointmentId)
    {
        throw new NotImplementedException();
    }

    public async Task<ResponseDTO> GetAppointmentDepositByAppointmentId(ClaimsPrincipal User, Guid appointmentId)
    {
        try
        {
            var appointment =
                await _unitOfWork.AppointmentDepositRepository.GetAsync(ad => ad.AppointmentId == appointmentId);
            if (appointment == null)
            {
                return new ResponseDTO()
                {
                    Message = "Appointment was not found",
                    IsSuccess = false,
                    StatusCode = 404,
                    Result = null
                };
            }

            var appointmentDepositDto = _mapper.Map<GetAppointmentDepositDTO>(appointment);
            // Trả về kết quả thành công
            return new ResponseDTO()
            {
                Message = "Appointment deposit found successfully",
                IsSuccess = true,
                StatusCode = 200,
                Result = appointmentDepositDto
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


    public async Task<ResponseDTO> CreateAppointmentDeposit(ClaimsPrincipal User,
        CreateAppointmentDepositDTO createAppointmentDepositDto)
    {
        try
        {
            var appointment =
                await _unitOfWork.AppointmentRepository.GetAsync(
                    a => a.AppointmentId == createAppointmentDepositDto.AppointmentId);
            if (appointment == null)
            {
                return new ResponseDTO
                {
                    Message = "Appointment not found",
                    IsSuccess = false,
                    StatusCode = 404,
                    Result = null
                };
            }

            long appointmentDepositNumber = appointment.AppointmentNumber + 1;

            var appointmentDeposit = new AppointmentDeposit
            {
                AppointmentId = createAppointmentDepositDto.AppointmentId,
                DepositAmount = createAppointmentDepositDto.DepositAmount,
                DepositStatus = 0,
                DepositTime = DateTime.Now,
                AppointmentDepositNumber = appointmentDepositNumber
            };

            await _unitOfWork.AppointmentDepositRepository.AddAsync(appointmentDeposit);
            await _unitOfWork.SaveAsync();

            // Cập nhật AppointmentDepositNumber vào bảng Appointment
            var appointmentToUpdate =
                await _unitOfWork.AppointmentRepository.GetAppointmentById(createAppointmentDepositDto.AppointmentId);
            if (appointmentToUpdate != null)
            {
                appointmentToUpdate.AppointmentDepositNumbe = appointmentDepositNumber;
                _unitOfWork.AppointmentRepository.Update(appointmentToUpdate);
                await _unitOfWork.SaveAsync();
            }

            return new ResponseDTO
            {
                Message = "Appointment deposit created successfully",
                IsSuccess = true,
                StatusCode = 201,
                Result = appointmentDeposit
            };
        }
        catch (Exception e)
        {
            return new ResponseDTO
            {
                Message = e.Message,
                IsSuccess = false,
                StatusCode = 500,
                Result = null
            };
        }
    }

    public async Task<ResponseDTO> DeleteAppointmentDeposit(ClaimsPrincipal User, Guid appointmentDepositId)
    {
        try
        {
            var appointmentDeposit =
                await _unitOfWork.AppointmentDepositRepository.GetAsync(ad => ad.DepositId == appointmentDepositId);
            if (appointmentDeposit == null)
            {
                return new ResponseDTO()
                {
                    Result = "",
                    StatusCode = 404,
                    Message = "Appointment deposit not found",
                    IsSuccess = false
                };
            }
            
            var appointmentId = appointmentDeposit.AppointmentId;
            var appointment = await _unitOfWork.AppointmentRepository.GetAsync(a => a.AppointmentId == appointmentId);
            if (appointment == null)
            {
                return new ResponseDTO()
                {
                    Result = "",
                    StatusCode = 404,
                    Message = "Appointment not found",
                    IsSuccess = false
                };
            }
            
            //Xóa AppointmentDeposit
            _unitOfWork.AppointmentDepositRepository.Remove(appointmentDeposit);
            //Xóa Appointment
            _unitOfWork.AppointmentRepository.Remove(appointment);
            await _unitOfWork.SaveAsync();

            return new ResponseDTO()
            {
                Result = "",
                StatusCode = 200,
                Message = "Appointment deposit deleted successfully",
                IsSuccess = true
            };
        }
        catch (Exception e)
        {
            return new ResponseDTO()
            {
                Result = "",
                StatusCode = 500,
                Message = e.Message,
                IsSuccess = false
            };
        }
    }
}