using System.Security.Claims;
using AutoMapper;
using KoiVeterinaryServiceCenter.DataAccess.IRepository;
using KoiVeterinaryServiceCenter.Model.DTO.Slot;
using KoiVeterinaryServiceCenter.Models.Domain;
using KoiVeterinaryServiceCenter.Models.DTO;
using KoiVeterinaryServiceCenter.Services.IServices;

namespace KoiVeterinaryServiceCenter.Services.Services;

public class SlotService : ISlotService
{
    private readonly IUnitOfWork _unitOfWork;
    private IMapper _mapper;

    public SlotService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResponseDTO> GetSlots
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
            List<Slot> slots = new List<Slot>();

            // Lấy tất cả các Slot từ repository
            var allSlots = await _unitOfWork.SlotRepository.GetAllAsync(includeProperties: "Doctor");

            // Bộ lọc
            if (!string.IsNullOrEmpty(filterOn) && !string.IsNullOrEmpty(filterQuery))
            {
                switch (filterOn.Trim().ToLower())
                {
                    case "appointmentdate":
                        if (DateTime.TryParse(filterQuery, out DateTime appointmentDate))
                        {
                            slots = allSlots.Where(x => x.AppointmentDate.Date == appointmentDate.Date).ToList();
                        }
                        break;

                    case "isbooked":
                        if (bool.TryParse(filterQuery, out bool isBooked))
                        {
                            slots = allSlots.Where(x => x.IsBooked == isBooked).ToList();
                        }

                        break;

                    default:
                        slots = allSlots.ToList(); // Nếu không có bộ lọc, lấy tất cả
                        break;
                }
            }
            else
            {
                slots = allSlots.ToList(); // Nếu không có bộ lọc, lấy tất cả
            }

            // Sắp xếp
            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy.Trim().ToLower())
                {
                    case "starttime":
                        slots = isAscending == true
                            ? slots.OrderBy(x => x.StartTime).ToList()
                            : slots.OrderByDescending(x => x.StartTime).ToList();
                        break;

                    case "endtime":
                        slots = isAscending == true
                            ? slots.OrderBy(x => x.EndTime).ToList()
                            : slots.OrderByDescending(x => x.EndTime).ToList();
                        break;

                    case "appointmentdate":
                        slots = isAscending == true
                            ? slots.OrderBy(x => x.AppointmentDate).ToList()
                            : slots.OrderByDescending(x => x.AppointmentDate).ToList();
                        break;

                    default:
                        break; // Không sắp xếp nếu không có trường hợp hợp lệ
                }
            }

            // Phân trang
            if (pageNumber > 0 && pageSize > 0)
            {
                var skipResult = (pageNumber - 1) * pageSize;
                slots = slots.Skip(skipResult).Take(pageSize).ToList();
            }

            if (slots == null || !slots.Any())
            {
                return new ResponseDTO()
                {
                    Message = "No slots found.",
                    Result = null,
                    IsSuccess = false,
                    StatusCode = 404
                };
            }

            return new ResponseDTO()
            {
                Message = "Slots retrieved successfully.",
                Result = slots,
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

    public async Task<ResponseDTO> GetSlot(ClaimsPrincipal User, Guid SlotId)
    {
        try
        {
            var SlotID =
                await _unitOfWork.SlotRepository.GetSlotById(SlotId);

            if (SlotID is null)
            {
                return new ResponseDTO()
                {
                    Result = "",
                    Message = "Slot was not found",
                    IsSuccess = true,
                    StatusCode = 404
                };
            }

            GetSlotDTO slotDto;
            try
            {
                slotDto = _mapper.Map<GetSlotDTO>(SlotID);
            }
            catch (AutoMapperMappingException e)
            {
                // Log the mapping error
                // Consider logging e.Message or e.InnerException for more details
                return new ResponseDTO()
                {
                    Result = null,
                    Message = "Failed to map slot to GetSlotDTO",
                    IsSuccess = false,
                    StatusCode = 500
                };
            }

            return new ResponseDTO()
            {
                Result = slotDto,
                Message = "Get slot successfully",
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

    public async Task<ResponseDTO> CreateSlot(ClaimsPrincipal User, CreateSlotDTO createSlotDto)
    {
        try
        {
            //Map DTO qua entity Level
            Slot slots = new Slot()
            {
                StartTime = createSlotDto.StartTime,
                EndTime = createSlotDto.EndTime,
                AppointmentDate = createSlotDto.AppointmentDate,
                IsBooked = createSlotDto.IsBooked,
                CreatedTime = DateTime.Now,
                UpdatedTime = null,
                CreatedBy = User.Identity.Name,
                UpdatedBy = ""
            };

            //thêm slot mới
            await _unitOfWork.SlotRepository.AddAsync(slots);
            var save = await _unitOfWork.SaveAsync();
            if (save <= 0)
            {
                return new ResponseDTO
                {
                    Message = "Failed to save slot",
                    Result = null,
                    IsSuccess = false,
                    StatusCode = 400
                };
            }

            return new ResponseDTO()
            {
                Message = "Slot created successfully",
                Result = slots,
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

    public async Task<ResponseDTO> UpdateSlot(ClaimsPrincipal User, UpdateSlotDTO updateSlotDto)
    {
        try
        {
            // kiểm tra xem có ID trong database không
            var slotID = await _unitOfWork.SlotRepository.GetAsync(c => c.SlotId == updateSlotDto.SlotId);

            // kiểm tra xem có null không
            if (slotID == null)
            {
                return new ResponseDTO
                {
                    Message = "Slot not found",
                    Result = null,
                    IsSuccess = false,
                    StatusCode = 404
                };
            }

            // Nếu có DoctorId, truy vấn Doctor từ database
            if (updateSlotDto.DoctorId.HasValue)
            {
                var doctor =
                    await _unitOfWork.DoctorRepository.GetAsync(d => d.DoctorId == updateSlotDto.DoctorId.Value);

                if (doctor == null)
                {
                    return new ResponseDTO
                    {
                        Message = "Doctor not found",
                        Result = null,
                        IsSuccess = false,
                        StatusCode = 404
                    };
                }
            }

            // cập nhật thông tin danh mục
            slotID.StartTime = updateSlotDto.StartTime;
            slotID.EndTime = updateSlotDto.EndTime;
            slotID.IsBooked = updateSlotDto.IsBooked;
            slotID.UpdatedBy = User.Identity.Name;
            slotID.UpdatedTime = DateTime.Now;


            // thay đổi dữ liệu
            _unitOfWork.SlotRepository.Update(slotID);

            // lưu thay đổi vào cơ sở dữ liệu
            var save = await _unitOfWork.SaveAsync();
            if (save <= 0)
            {
                return new ResponseDTO
                {
                    Message = "Failed to update slot",
                    Result = null,
                    IsSuccess = false,
                    StatusCode = 400
                };
            }

            return new ResponseDTO
            {
                Message = "Slot updated successfully",
                Result = slotID,
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

    public async Task<ResponseDTO> DeleteSlot(ClaimsPrincipal User, Guid slotId)
    {
        try
        {
            // kiểm tra xem có ID trong database không
            var slotID = await _unitOfWork.SlotRepository.GetAsync(c => c.SlotId == slotId);

            if (slotID == null)
            {
                return new ResponseDTO
                {
                    Message = "Slot not found",
                    Result = null,
                    IsSuccess = false,
                    StatusCode = 400
                };
            }

            _unitOfWork.SlotRepository.Remove(slotID);
            await _unitOfWork.SaveAsync();

            return new ResponseDTO
            {
                Message = "Slot deleted successfully",
                Result = slotID,
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