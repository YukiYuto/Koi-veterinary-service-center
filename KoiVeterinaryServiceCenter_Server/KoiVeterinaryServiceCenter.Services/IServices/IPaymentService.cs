using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using KoiVeterinaryServiceCenter.Model.DTO;
using Net.payOS.Types;

namespace KoiVeterinaryServiceCenter.Services.IServices
{
    public interface IPaymentService
    {
        Task<ResponseDTO> CreatePaymentLink(ClaimsPrincipal User, CreatePaymentLinkDTO getPaymentLink);
        Task<ResponseDTO> UpdatePaymentStatus(ClaimsPrincipal User, Guid paymentTransactionId);
    }
}
