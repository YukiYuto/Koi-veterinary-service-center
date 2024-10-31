using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KoiVeterinaryServiceCenter.Models.DTO;

namespace KoiVeterinaryServiceCenter.Services.IServices
{
    public interface IDashBoardService
    {
        Task<ResponseDTO> TotalRevenueOfDay(DateOnly date);
        Task<ResponseDTO> TotalRevenueOfMonth(int month, int year);
        Task<ResponseDTO> RevenuaOfMonthList(int month, int year);
    }
}
