using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using KoiVeterinaryServiceCenter.Model.DTO;

namespace KoiVeterinaryServiceCenter.Services.IServices
{
    public interface IDoctorServicesService
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
        Task<ResponseDTO> CreateDoctorService(ClaimsPrincipal User, CreateDoctorServicesDTO createDoctorServiceDTO);
        Task<ResponseDTO> GetDoctorServiceById(ClaimsPrincipal User, Guid doctorServiceId);
        Task<ResponseDTO> UpdateDoctorService(ClaimsPrincipal User, UpdateDoctorServicesDTO updateDoctorServiceDTO);
        Task<ResponseDTO> DeleteDoctorService(ClaimsPrincipal User, Guid doctorServiceId);

    }
}
