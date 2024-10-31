using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using KoiVeterinaryServiceCenter.Models.DTO;
using KoiVeterinaryServiceCenter.Models.DTO.Doctor;
using KoiVeterinaryServiceCenter.Models.DTO.Slot;

namespace KoiVeterinaryServiceCenter.Services.IServices
{
    public interface IDoctorService
    {
        Task<ResponseDTO> GetAll(
                ClaimsPrincipal User,
                string? filterOn,
                string? filterQuery,
                string? sortBy,
                bool? isAscending,
                int pageNumber,
                int pageSize
            );
        Task<ResponseDTO> GetDoctorById(ClaimsPrincipal User, Guid id);
        Task<ResponseDTO> UpdateDoctorById(ClaimsPrincipal User, UpdateDoctorDTO updateDoctorDTO);
        Task<ResponseDTO> DeleteDoctorById(ClaimsPrincipal User, Guid id);
        Task<ResponseDTO> CreateGoogleMeetLink(ClaimsPrincipal User, GoogleMeetLinkDTO googleMeetLinkDTO);
        Task<ResponseDTO> UpdateGoogleMeetLink(ClaimsPrincipal User, GoogleMeetLinkDTO googleMeetLinkDTO);
        Task<ResponseDTO> GetAllDoctorBySlot(ClaimsPrincipal User, GetAllDoctorBySlotDTO getDoctorBySlotDTO);
    }
}
