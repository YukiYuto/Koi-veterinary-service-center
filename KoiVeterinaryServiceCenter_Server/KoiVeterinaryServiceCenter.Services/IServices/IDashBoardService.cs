using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using KoiVeterinaryServiceCenter.Models.DTO;

namespace KoiVeterinaryServiceCenter.Services.IServices
{
    public interface IDashBoardService
    {
        Task<ResponseDTO> TotalRevenueOfDay(ClaimsPrincipal User, DateOnly date);
        Task<ResponseDTO> TotalRevenueOfMonth(ClaimsPrincipal User, int month, int year);
        Task<ResponseDTO> RevenuaOfMonthList(ClaimsPrincipal User, int month, int year);
    }
}
