using AutoMapper;
using KoiVeterinaryServiceCenter.DataAccess.IRepository;
using KoiVeterinaryServiceCenter.Models.Domain;
using KoiVeterinaryServiceCenter.Models.DTO.Appointment;
using KoiVeterinaryServiceCenter.Models.DTO;
using KoiVeterinaryServiceCenter.Services.IServices;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace KoiVeterinaryServiceCenter.Services.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public AppointmentService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }

        // Lấy danh sách appointment
        public async Task<ResponseDTO> GetAppointments
        (
            ClaimsPrincipal User,
            string? filterOn,
            string? filterQuery,
            string? sortBy,
            int pageNumber = 0,
            int pageSize = 0)
        {
            try
            {
                // Lấy tất cả cuộc hẹn
                var allAppointments = (await _unitOfWork.AppointmentRepository
                    .GetAllAsync());

                // Khởi tạo danh sách appointments
                List<Appointment> appointments = allAppointments.ToList();

                // Filter Query
                if (!string.IsNullOrEmpty(filterOn) && !string.IsNullOrEmpty(filterQuery))
                {
                    appointments = appointments.Where(x =>
                    {
                        switch (filterOn.Trim().ToLower())
                        {
                            case "bookingstatus":
                                if (int.TryParse(filterQuery, out int bookingStatus))
                                {
                                    return x.BookingStatus == bookingStatus;
                                }

                                return false;

                            case "totalamount":
                                if (double.TryParse(filterQuery, out double amount))
                                {
                                    return x.TotalAmount == amount;
                                }

                                return false;

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
                        case "bookingstatus":
                            appointments = appointments.OrderBy(x => x.BookingStatus).ToList();
                            break;

                        case "totalamount":
                            appointments = appointments.OrderBy(x => x.TotalAmount).ToList();
                            break;

                        default:
                            break; // Nếu không có trường hợp nào khớp, giữ nguyên danh sách
                    }
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
                appointmentDto = _mapper.Map<GetAppointmentDTO>(AppointmentId);
                
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
                // Lấy ID người dùng đã đăng nhập từ Claims
                var loggedInUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                // Kiểm tra xem ID người dùng đã đăng nhập có trùng với CustomerId không
                if (loggedInUserId != createAppointmentDto.CustomerId)
                {
                    return new ResponseDTO()
                    {
                        Result = "",
                        Message = "You do not have permission to create this appointment.",
                        IsSuccess = false,
                        StatusCode = 403
                    };
                }

                var service =
                    await _unitOfWork.ServiceRepository.GetAsync(c => c.ServiceId == createAppointmentDto.ServiceId);
                if (service == null)
                {
                    return new ResponseDTO()
                    {
                        Result = "",
                        Message = "service was not found",
                        IsSuccess = true,
                        StatusCode = 404
                    };
                }
                
                var slot = await _unitOfWork.SlotRepository.GetAsync(c => c.SlotId == createAppointmentDto.SlotId);
                if (slot == null)
                {
                    return new ResponseDTO()
                    {
                        Result = "",
                        Message = "slot was not found",
                        IsSuccess = true,
                        StatusCode = 404
                    };
                }

                // Lấy số AppointmentNumber lớn nhất hiện có và tăng nó lên 1
                long appointmentNumber = await _unitOfWork.AppointmentRepository.GenerateUniqueNumberAsync();
                
                //Map DTO qua entity Level
                Appointment appointments = new Appointment()
                {
                    SlotId = createAppointmentDto.SlotId,
                    ServiceId = createAppointmentDto.ServiceId,
                    TotalAmount = createAppointmentDto.TotalAmount,
                    BookingStatus = 0,
                    Description = createAppointmentDto.Description,
                    CustomerId = createAppointmentDto.CustomerId,
                    CreateTime = createAppointmentDto.CreateTime,
                    AppointmentNumber = appointmentNumber
                };

                //thêm appointment mới
                await _unitOfWork.AppointmentRepository.AddAsync(appointments);
                await _unitOfWork.SaveAsync();

                return new ResponseDTO()
                {
                    Message = "Appointment created successfully",
                    Result = appointments,
                    IsSuccess = true,
                    StatusCode = 201,
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
        public async Task<ResponseDTO> DeleteAppointment(string customerId)
        {
            try
            {
                var appointments = await _unitOfWork.AppointmentRepository
                    .GetAllAsync(c => c.CustomerId == customerId && c.BookingStatus == 0);

                // Kiểm tra nếu không có appointment nào  
                if (appointments == null || !appointments.Any())
                {
                    return new ResponseDTO
                    {
                        Message = "No appointments found for this customer with status 0",
                        Result = null,
                        IsSuccess = false,
                        StatusCode = 404
                    };
                }

                // Xóa theo lô  
                const int batchSize = 10;
                var totalAppointments = appointments.ToList();

                for (int i = 0; i < totalAppointments.Count; i += batchSize)
                {
                    var batch = totalAppointments.Skip(i).Take(batchSize);
                    foreach (var appointment in batch)
                    {
                        _unitOfWork.AppointmentRepository.Remove(appointment);
                    }

                    await _unitOfWork.SaveAsync();
                }

                return new ResponseDTO
                {
                    Message = "Deleted successfully",
                    Result = null,
                    IsSuccess = true,
                    StatusCode = 200
                };
            }
            catch (Exception e)
            {
                return new ResponseDTO
                {
                    Message = "An error occurred while deleting appointments: " + e.Message,
                    Result = null,
                    IsSuccess = false,
                    StatusCode = 500
                };
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        public async Task<ResponseDTO> GetAppointmentByUserId(ClaimsPrincipal User)
        {
            try
            {
                var loggedInUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(loggedInUserId))
                {
                    return new ResponseDTO()
                    {
                        Message = "User is not authenticated.",
                        Result = null,
                        IsSuccess = false,
                        StatusCode = 403
                    };
                }

                // Lấy danh sách các cuộc hẹn của người dùng
                var appointments = await _unitOfWork.AppointmentRepository.GetAppointmentsByUserId(loggedInUserId);

                if (appointments == null || !appointments.Any())
                {
                    return new ResponseDTO()
                    {
                        Message = "No appointments found for this user.",
                        Result = null,
                        IsSuccess = false,
                        StatusCode = 404
                    };
                }

                // Tạo danh sách các DTO
                var appointmentDtos = new List<GetAppointmentDTO>();

                foreach (var appointment in appointments)
                {
                    var customer = await _userManager.FindByIdAsync(appointment.CustomerId);
                    var appointmentDto = _mapper.Map<GetAppointmentDTO>(appointment);

                    // Nếu tìm thấy customer, gán thêm CustomerName vào DTO
                    if (customer != null)
                    {
                        appointmentDto.CustomerName = customer.FullName; // Gán FullName vào DTO
                    }

                    // Thêm DTO vào danh sách
                    appointmentDtos.Add(appointmentDto);
                }

                return new ResponseDTO()
                {
                    Message = "Appointments retrieved successfully.",
                    Result = appointmentDtos,
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

        public async Task<ResponseDTO> GetAppointmentMeetLinkByUserId(ClaimsPrincipal User)
        {
            try
            {
                var loggedInUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(loggedInUserId))
                {
                    return new ResponseDTO()
                    {
                        Message = "User is not authenticated.",
                        Result = null,
                        IsSuccess = false,
                        StatusCode = 403
                    };
                }

                // Lấy danh sách các cuộc hẹn của người dùng
                var appointments = await _unitOfWork.AppointmentRepository.GetAppointmentsByUserId(loggedInUserId);

                if (appointments == null || !appointments.Any())
                {
                    return new ResponseDTO()
                    {
                        Message = "No appointments found for this user.",
                        Result = null,
                        IsSuccess = false,
                        StatusCode = 404
                    };
                }

                var meetLinks = new List<string>();

                foreach (var appointment in appointments)
                {
                    // Lấy SlotId từ Appointments
                    var slot = await _unitOfWork.SlotRepository.GetSlotById(appointment.SlotId);

                    if (slot == null)
                    {
                        continue;
                    }

                    // Lấy DoctorSchedulesId từ Slot
                    var doctorSchedule = await _unitOfWork.DoctorSchedulesRepository.GetDoctorScheduleById(slot.DoctorSchedulesId);

                    if (doctorSchedule == null)
                    {
                        continue;
                    }

                    // Lấy DoctorId từ DoctorSchedules
                    var doctor = await _unitOfWork.DoctorRepository.GetDoctorById(doctorSchedule.DoctorId);

                    if (doctor == null || string.IsNullOrEmpty(doctor.GoogleMeetLink))
                    {
                        continue;
                    }

                    // Thêm GoogleMeetLink vào danh sách
                    meetLinks.Add(doctor.GoogleMeetLink);
                }

                return new ResponseDTO()
                {
                    Message = "Google Meet links retrieved successfully.",
                    Result = meetLinks,
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
}