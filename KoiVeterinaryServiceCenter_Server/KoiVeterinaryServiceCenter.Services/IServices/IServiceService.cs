using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using KoiVeterinaryServiceCenter.Model.DTO;

namespace KoiVeterinaryServiceCenter.Services.IServices
{
    public interface IServiceService
    {
        Task<ResponseDTO> GetServiceById(ClaimsPrincipal User, Guid serviceId);
        Task<ResponseDTO> CreateService(ClaimsPrincipal User, CreateServiceDTO createServiceDTO);
        Task<ResponseDTO> UpdateService(ClaimsPrincipal User, UpdateServiceDTO updateServiceDTO);
        Task<ResponseDTO> DeleteService(ClaimsPrincipal User, Guid serviceId);
    }
}
