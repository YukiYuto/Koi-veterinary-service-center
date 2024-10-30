using System.Security.Claims;
using AutoMapper;
using KoiVeterinaryServiceCenter.DataAccess.IRepository;
using KoiVeterinaryServiceCenter.Model.DTO.Slot;
using KoiVeterinaryServiceCenter.Models.Domain;
using KoiVeterinaryServiceCenter.Models.DTO;
using KoiVeterinaryServiceCenter.Models.DTO.Slot;
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
    int pageNumber = 1,
    int pageSize = 10
    )
    {
        try
        {
            var slots = await _unitOfWork.SlotRepository.GetAllSlotWithDoctor();

            // Kiểm tra nếu danh sách slots là null hoặc rỗng  
            if (!slots.Any())
            {
                return new ResponseDTO()
                {
                    Message = "There are no slots",
                    IsSuccess = true,
                    StatusCode = 404,
                    Result = null
                };
            }

            var listSlots = slots.ToList();

            // Filter Query  
            if (!string.IsNullOrEmpty(filterOn) && !string.IsNullOrEmpty(filterQuery))
            {
                switch (filterOn.Trim().ToLower())
                {
                    case "isbooked":
                        if (int.TryParse(filterQuery, out int isBooked))
                        {
                            listSlots = listSlots.Where(x => x.IsBooked == isBooked).ToList();
                        }
                        break;

                    case "starttime":
                        if (TimeSpan.TryParse(filterQuery, out TimeSpan startTime))
                        {
                            listSlots = listSlots.Where(x => x.StartTime == startTime).ToList();
                        }
                        break;

                    case "endtime":
                        if (TimeSpan.TryParse(filterQuery, out TimeSpan endTime))
                        {
                            listSlots = listSlots.Where(x => x.EndTime == endTime).ToList();
                        }
                        break;

                    default:
                        break;
                }
            }

            // Sort Query  
            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy.Trim().ToLower())
                {
                    case "starttime":
                        listSlots = isAscending == true
                            ? listSlots.OrderBy(x => x.StartTime).ToList()
                            : listSlots.OrderByDescending(x => x.StartTime).ToList();
                        break;

                    case "endtime":
                        listSlots = isAscending == true
                            ? listSlots.OrderBy(x => x.EndTime).ToList()
                            : listSlots.OrderByDescending(x => x.EndTime).ToList();
                        break;

                    default:
                        // If no valid `sortBy` value is provided, default to sorting by StartTime descending  
                        listSlots = listSlots.OrderByDescending(x => x.StartTime).ToList();
                        break;
                }
            }
            else
            {
                // If no `sortBy` is specified, default to sorting by StartTime descending  
                listSlots = listSlots.OrderByDescending(x => x.StartTime).ToList();
            }

            // Pagination  
            if (pageNumber > 0 && pageSize > 0)
            {
                var skipResult = (pageNumber - 1) * pageSize;
                listSlots = listSlots.Skip(skipResult).Take(pageSize).ToList();
            }

            var slotDto = listSlots.Select(slots => new GetSlotDTO()
            {
                SlotId = slots.SlotId,
                DoctorId = slots.DoctorSchedules.DoctorId,
                DoctorSchedulesId = slots.DoctorSchedulesId,
                StartTime = slots.StartTime,
                EndTime = slots.EndTime,
                IsBooked = slots.IsBooked,
                
                CreatedTime = slots.CreatedTime,
                CreatedBy = slots.CreatedBy,
            }).ToList();

            return new ResponseDTO()
            {
                Message = "Slots retrieved successfully.",
                Result = slotDto,
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
                DoctorSchedulesId = createSlotDto.DoctorSchedulesId,
                StartTime = createSlotDto.StartTime,
                EndTime = createSlotDto.EndTime,
                IsBooked = 0,
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
            slotID.IsBooked = 2;
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

            slotID.IsBooked = 2;
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