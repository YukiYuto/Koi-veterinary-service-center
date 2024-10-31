using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using KoiVeterinaryServiceCenter.Models.DTO;
using KoiVeterinaryServiceCenter.Models.DTO.Payment;
using Net.payOS.Types;

namespace KoiVeterinaryServiceCenter.Services.IServices
{
    public interface IPaymentService
    {
        Task<ResponseDTO> CreatePayOSPaymentLink(ClaimsPrincipal User, CreatePaymentLinkDTO getPaymentLink);
        Task<ResponseDTO> UpdatePayOSPaymentStatus(ClaimsPrincipal User, Guid paymentTransactionId);
        Task<ResponseDTO> CancelPayOSPaymentLink(ClaimsPrincipal User, Guid paymentTransactionId, string cancellationReason);
        Task<ResponseDTO> UpdatePayOSPaymentStatusForDepositPart1(ClaimsPrincipal User, Guid paymentTransacId);
        Task<ResponseDTO> CreatePayOSLinkForDepositPart1(ClaimsPrincipal User, CreatePaymentLinkDTO createPaymentLinkDTO);
    }
}
