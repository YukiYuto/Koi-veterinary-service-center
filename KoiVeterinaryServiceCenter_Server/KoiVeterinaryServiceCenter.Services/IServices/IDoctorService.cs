using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using KoiVeterinaryServiceCenter.Model.DTO;

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
        Task<ResponseDTO> GetDoctorById(Guid id);
        Task<ResponseDTO> UpdateDoctorById(UpdateDoctorDTO updateDoctorDTO);
        Task<ResponseDTO> DeleteDoctorById(Guid id);
    }
}
