using System.Security.Claims;
using KoiVeterinaryServiceCenter.Model.DTO.Slot;
using KoiVeterinaryServiceCenter.Models.DTO;
using KoiVeterinaryServiceCenter.Models.DTO.Slot;

namespace KoiVeterinaryServiceCenter.Services.IServices;

public interface ISlotService
{
    Task<ResponseDTO> GetSlots
    (
        ClaimsPrincipal User,
        string? filterOn,
        string? filterQuery,
        string? sortBy,
        bool? isAscending,
        int pageNumber = 0,
        int pageSize = 0
    );

    Task<ResponseDTO> GetSlot(ClaimsPrincipal User, Guid SlotId);
    Task<ResponseDTO> CreateSlot(ClaimsPrincipal User, CreateSlotDTO createSlotDto);
    Task<ResponseDTO> UpdateSlot(ClaimsPrincipal User, UpdateSlotDTO updateSlotDto);
    Task<ResponseDTO> DeleteSlot(ClaimsPrincipal User, Guid slotId);
}