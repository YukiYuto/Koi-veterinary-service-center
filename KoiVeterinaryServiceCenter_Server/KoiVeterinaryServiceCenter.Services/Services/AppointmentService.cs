using AutoMapper;
using KoiVeterinaryServiceCenter.DataAccess.IRepository;
using KoiVeterinaryServiceCenter.Model.Domain;
using KoiVeterinaryServiceCenter.Model.DTO;
using KoiVeterinaryServiceCenter.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace KoiVeterinaryServiceCenter.Services.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public AppointmentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // Lấy danh sách appointment
        public async Task<ResponseDTO> GetAppointments
(
    ClaimsPrincipal User,
    string? filterOn,
    string? filterQuery,
    string? sortBy,
    bool? isAscending,
    int pageNumber = 0,
    int pageSize = 0)
{
    try
    {
        // Lấy tất cả cuộc hẹn
        var allAppointments = (await _unitOfWork.AppointmentRepository
            .GetAllAsync(includeProperties: "DoctorRating")).ToList();

        // Khởi tạo danh sách appointments
        List<Appointment> appointments = allAppointments;

        // Filter Query
        if (!string.IsNullOrEmpty(filterOn) && !string.IsNullOrEmpty(filterQuery))
        {
            appointments = appointments.Where(x =>
            {
                switch (filterOn.Trim().ToLower())
                {
                    case "appointmentdate":
                        if (DateTime.TryParse(filterQuery, out DateTime filterDate))
                        {
                            return x.AppointmentDate.Date == filterDate.Date;
                        }
                        return false;

                    case "bookingstatus":
                        return x.BookingStatus.Contains(filterQuery, StringComparison.CurrentCultureIgnoreCase);

                    case "appointmentstatus":
                        return x.AppointmentStatus.Contains(filterQuery, StringComparison.CurrentCultureIgnoreCase);

                    case "totalamount":
                        if (double.TryParse(filterQuery, out double amount))
                        {
                            return x.TotalAmount == amount;
                        }
                        return false;

                    case "type":
                        return x.Type.Contains(filterQuery, StringComparison.CurrentCultureIgnoreCase);

                    default:
                        return true; // Trả về tất cả nếu không có điều kiện khớp
                }
            }).ToList();
        }

        // Sắp xếp nếu có yêu cầu
        if (!string.IsNullOrEmpty(sortBy))
        {
            appointments = sortBy.Trim().ToLower() switch
            {
                "appointmentdate" => isAscending == true
                    ? appointments.OrderBy(x => x.AppointmentDate).ToList()
                    : appointments.OrderByDescending(x => x.AppointmentDate).ToList(),
                
                "bookingstatus" => isAscending == true
                    ? appointments.OrderBy(x => x.BookingStatus).ToList()
                    : appointments.OrderByDescending(x => x.BookingStatus).ToList(),

                "appointmentstatus" => isAscending == true
                    ? appointments.OrderBy(x => x.AppointmentStatus).ToList()
                    : appointments.OrderByDescending(x => x.AppointmentStatus).ToList(),

                "totalamount" => isAscending == true
                    ? appointments.OrderBy(x => x.TotalAmount).ToList()
                    : appointments.OrderByDescending(x => x.TotalAmount).ToList(),

                "type" => isAscending == true
                    ? appointments.OrderBy(x => x.Type).ToList()
                    : appointments.OrderByDescending(x => x.Type).ToList(),

                _ => appointments // Nếu không có trường hợp nào khớp, giữ nguyên danh sách
            };
        }

        // Phân trang
        if (pageNumber > 0 && pageSize > 0)
        {
            var skipResult = (pageNumber - 1) * pageSize;
            appointments = appointments.Skip(skipResult).Take(pageSize).ToList();
        }

        if (appointments == null || !appointments.Any())
        {
            return new ResponseDTO()
            {
                Message = "There are no appointments",
                Result = null,
                IsSuccess = false,
                StatusCode = 404
            };
        }

        return new ResponseDTO()
        {
            Message = "Get appointments successfully",
            Result = appointments,
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

        // Lấy danh sách appointment theo id
        public async Task<ResponseDTO> GetAppointment(ClaimsPrincipal User, Guid appointmentId)
        {
            try
            {
                var AppointmentId =
                    await _unitOfWork.AppointmentRepository.GetAppointmentById(appointmentId);

                if (AppointmentId is null)
                {
                    return new ResponseDTO()
                    {
                        Result = "",
                        Message = "Appointment was not found",
                        IsSuccess = true,
                        StatusCode = 404
                    };
                }

                GetAppointmentDTO appointmentDto;
                try
                {
                    appointmentDto = _mapper.Map<GetAppointmentDTO>(AppointmentId);
                }
                catch (AutoMapperMappingException e)
                {
                    // Log the mapping error
                    // Consider logging e.Message or e.InnerException for more details
                    return new ResponseDTO()
                    {
                        Result = null,
                        Message = "Failed to map Appointment to GetAppointmentDTO",
                        IsSuccess = false,
                        StatusCode = 500
                    };
                }

                return new ResponseDTO()
                {
                    Result = appointmentDto,
                    Message = "Get appointment successfully",
                    IsSuccess = true,
                    StatusCode = 200
                };
            }
            catch (Exception e)
            {
                return new ResponseDTO()
                {
                    Result = null,
                    Message = e.Message,
                    IsSuccess = true,
                    StatusCode = 500
                };
            }
        }

        // Tạo mới appointment
        public async Task<ResponseDTO> CreateAppointment(ClaimsPrincipal User,
            CreateAppointmentDTO createAppointmentDto)
        {
            try
            {
                //Map DTO qua entity Level
                Appointment appointments = new Appointment()
                {
                    PetId = createAppointmentDto.PetId,
                    SlotId = createAppointmentDto.SlotId,
                    DoctorRatingId = createAppointmentDto.DoctorRatingId,
                    ServiceId = createAppointmentDto.ServiceId,
                    AppointmentDate = createAppointmentDto.AppointmentDate,
                    BookingStatus = createAppointmentDto.BookingStatus,
                    AppointmentStatus = createAppointmentDto.AppointmentStatus,
                    TotalAmount = createAppointmentDto.TotalAmount,
                    Type = createAppointmentDto.Type
                };

                //thêm appointment mới
                await _unitOfWork.AppointmentRepository.AddAsync(appointments);
                await _unitOfWork.SaveAsync();

                return new ResponseDTO()
                {
                    Message = "Appointment created successfully",
                    Result = appointments,
                    IsSuccess = true,
                    StatusCode = 200,
                };
            }
            catch (Exception e)
            {
                return new ResponseDTO
                {
                    Message = e.Message,
                    Result = null,
                    IsSuccess = false,
                    StatusCode = 500
                };
            }
        }

        // Cập nhật appointment
        public async Task<ResponseDTO> UpdateAppointment(ClaimsPrincipal User,
            UpdateAppointmentDTO updateAppointmentDto)
        {
            try
            {
                // kiểm tra xem có 
                var appointmentID =
                    await _unitOfWork.AppointmentRepository.GetAsync(c =>
                        c.AppointmentId == updateAppointmentDto.AppointmentId);

                // kiểm tra xem có null không
                if (appointmentID == null)
                {
                    return new ResponseDTO
                    {
                        Message = "Appointment not found",
                        Result = null,
                        IsSuccess = false,
                        StatusCode = 404
                    };
                }

                // cập nhật thông tin danh mục
                appointmentID.PetId = updateAppointmentDto.PetId;
                appointmentID.SlotId = updateAppointmentDto.SlotId;
                appointmentID.DoctorRatingId = updateAppointmentDto.DoctorRatingId;
                appointmentID.ServiceId = updateAppointmentDto.ServiceId;
                appointmentID.AppointmentDate = updateAppointmentDto.AppointmentDate;
                appointmentID.BookingStatus = updateAppointmentDto.BookingStatus;
                appointmentID.AppointmentStatus = updateAppointmentDto.AppointmentStatus;
                appointmentID.TotalAmount = updateAppointmentDto.TotalAmount;
                appointmentID.Type = updateAppointmentDto.Type;


                // thay đổi dữ liệu
                _unitOfWork.AppointmentRepository.Update(appointmentID);

                // lưu thay đổi vào cơ sở dữ liệu
                var save = await _unitOfWork.SaveAsync();
                if (save <= 0)
                {
                    return new ResponseDTO
                    {
                        Message = "Failed to update appointment",
                        Result = null,
                        IsSuccess = false,
                        StatusCode = 400
                    };
                }

                return new ResponseDTO
                {
                    Message = "Appointment updated successfully",
                    Result = appointmentID,
                    IsSuccess = true,
                    StatusCode = 200
                };
            }
            catch (Exception e)
            {
                return new ResponseDTO
                {
                    Message = e.Message,
                    Result = null,
                    IsSuccess = false,
                    StatusCode = 500
                };
            }
        }

        // Xóa appointment
        public async Task<ResponseDTO> DeleteAppointment(ClaimsPrincipal User, Guid appointmentId)
        {
            try
            {
                // kiểm tra xem có appointment không
                var appointmentID =
                    await _unitOfWork.AppointmentRepository.GetAsync(c => c.AppointmentId == appointmentId);
                // kiểm tra xem có null không
                if (appointmentID == null)
                {
                    return new ResponseDTO
                    {
                        Message = "Appointment not found",
                        Result = null,
                        IsSuccess = false,
                        StatusCode = 404
                    };
                }

                // cập nhật status của appointment là 1
                appointmentID.BookingStatus = "1";

                _unitOfWork.AppointmentRepository.Update(appointmentID);
                await _unitOfWork.SaveAsync();

                return new ResponseDTO
                {
                    Message = "Appointment deleted successfully",
                    Result = null,
                    IsSuccess = true,
                    StatusCode = 200
                };
            }
            catch (Exception e)
            {
                return new ResponseDTO
                {
                    Message = e.Message,
                    Result = null,
                    IsSuccess = false,
                    StatusCode = 500
                };
            }
        }
    }
}