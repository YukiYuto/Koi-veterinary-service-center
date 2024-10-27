using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using KoiVeterinaryServiceCenter.Models.DTO;
using KoiVeterinaryServiceCenter.Models.DTO.Appointment;
using KoiVeterinaryServiceCenter.Models.DTO.Transaction;

namespace KoiVeterinaryServiceCenter.Services.IServices
{
    public interface ITransactionsService
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
        Task<ResponseDTO> CreateTransaction(ClaimsIdentity User, CreateTransactionDTO createTransactionDTO);
        Task<ResponseDTO> GetById(ClaimsPrincipal User, Guid transactionId);
    }
}
