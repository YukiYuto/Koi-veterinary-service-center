using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using KoiVeterinaryServiceCenter.Models.DTO;

namespace KoiVeterinaryServiceCenter.Services.IServices
{
    public interface ITransactionsService
    {
        Task<ResponseDTO> GetAll();
        Task<ResponseDTO> GetById(Guid transactionId);
        void Update (Transaction transaction); 
    }
}
