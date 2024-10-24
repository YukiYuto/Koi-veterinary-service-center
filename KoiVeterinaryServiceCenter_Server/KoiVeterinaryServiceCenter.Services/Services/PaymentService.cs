using Net.payOS;
using KoiVeterinaryServiceCenter.Services.IServices;
using Microsoft.Extensions.Configuration;
using Net.payOS.Types;
using KoiVeterinaryServiceCenter.Model.DTO;
using AutoMapper;
using KoiVeterinaryServiceCenter.DataAccess.IRepository;
using KoiVeterinaryServiceCenter.Model.Domain;
using System.Security.Claims;
using KoiVeterinaryServiceCenter.Utility.Constants;
using StackExchange.Redis;
using KoiVeterinaryServiceCenter.Models.DTO;

public class PaymentService : IPaymentService
{
    private readonly PayOS _payOS;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public PaymentService(IConfiguration configuration, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _payOS = new PayOS(
        configuration["Environment:PAYOS_CLIENT_ID"] ?? throw new Exception("Cannot find PAYOS_CLIENT_ID"),
        configuration["Environment:PAYOS_API_KEY"] ?? throw new Exception("Cannot find PAYOS_API_KEY"),
        configuration["Environment:PAYOS_CHECKSUM_KEY"] ?? throw new Exception("Cannot find PAYOS_CHECKSUM_KEY"),
        configuration["Environment:PAYOS_PARTNER_CODE"] ?? throw new Exception("Cannot find PAYOS_PARTNER_CODE")
    );
    }

    public async Task<ResponseDTO> CreatePayOSPaymentLink(ClaimsPrincipal User, CreatePaymentLinkDTO createPaymentLink)
    {
        /*try
        {
            OrderItems order = await _unitOfWork.OrderItemsRepository.GetById(createPaymentLink.OrderCode);
            if (order is null)
            {
                return new ResponseDTO()
                {
                    Message = "Order is not exist",
                    IsSuccess = false,
                    StatusCode = 404,
                    Result = null
                };
            }


            var items = new List<ItemData>()
            {
                new ItemData( name: order.ProductName, quantity: 1, price: order.Price)
            };


            var paymentData = new PaymentData(
                orderCode: createPaymentLink.OrderCode,
                amount: order.Price,
                buyerName: createPaymentLink.BuyerName,
                buyerEmail: createPaymentLink.BuyerEmail,
                buyerPhone: createPaymentLink.BuyerPhone,
                buyerAddress: createPaymentLink.BuyerAddress,
                description: order.Description,
                items: items,
                cancelUrl: createPaymentLink.CancelUrl,
                returnUrl: createPaymentLink.ReturnUrl
            );
            if (paymentData is null)
            {
                return new ResponseDTO()
                {
                    Message = "Payment is missing data",
                    IsSuccess = false,
                    StatusCode = 500,
                    Result = null
                };
            }

            CreatePaymentResult result = await _payOS.createPaymentLink(paymentData);

            PaymentTransactions paymentTransactions = new PaymentTransactions()
            {
                OrderCode = createPaymentLink.OrderCode,
                Amount = result.amount,
                Description = result.description,
                BuyerName = createPaymentLink.BuyerName,
                BuyerEmail = createPaymentLink.BuyerEmail,
                BuyerPhone = createPaymentLink.BuyerPhone,
                BuyerAddress = createPaymentLink.BuyerAddress,
                CancelUrl = paymentData.cancelUrl,
                ReturnUrl = paymentData.returnUrl,
                ExpiredAt = paymentData.expiredAt,
                Signature = paymentData.signature,
                CreatedAt = DateTime.Now,
                Status = StaticPayment.paymentStatusDefault
            };

            await _unitOfWork.PaymentTransactionsRepository.AddAsync(paymentTransactions);
            await _unitOfWork.SaveAsync();
            return new ResponseDTO()
            {
                Message = "Create payment link successfully",
                IsSuccess = true,
                StatusCode = 200,
                Result = result
            };
        }
        catch (Exception e)
        {
            return new ResponseDTO()
            {
                Message = e.Message,
                IsSuccess = false,
                StatusCode = 500,
                Result = null
            };
        }*/
        return null;
    }

    public async Task<ResponseDTO> UpdatePayOSPaymentStatus(ClaimsPrincipal User, Guid paymentTransactionId)
    {
        try
        {
            PaymentTransactions paymentTransactions = await _unitOfWork.PaymentTransactionsRepository.GetById(paymentTransactionId);
            if (paymentTransactions is null)
            {
                return new ResponseDTO()
                {
                    Message = "Payment transaction is not exist",
                    IsSuccess = false,
                    StatusCode = 404,
                    Result = null
                };
            }

            var paymentStatus = _payOS.getPaymentLinkInformation(paymentTransactions.OrderCode);

            if (paymentStatus != null)
            {
                paymentTransactions.Status = paymentStatus.Result.status;
            }

            _unitOfWork.PaymentTransactionsRepository.Update(paymentTransactions);
            await _unitOfWork.SaveAsync();

            return new ResponseDTO()
            {
                Message = "Update status successfully",
                IsSuccess = true,
                StatusCode = 200,
                Result = paymentStatus.Result

            };
        }
        catch (Exception e)
        {
            return new ResponseDTO()
            {
                Message = e.Message,
                IsSuccess = false,
                StatusCode = 500,
                Result = null
            };
        }
    }

    public async Task<ResponseDTO> CancelPayOSPaymentLink(ClaimsPrincipal User, Guid paymentTransactionId, string cancellationReason)
    {
        try
        {
            PaymentTransactions paymentTransactions = await _unitOfWork.PaymentTransactionsRepository.GetById(paymentTransactionId);

            if (paymentTransactions is null)
            {
                return new ResponseDTO()
                {
                    Message = "Cannot find payment transaction ID",
                    IsSuccess = false,
                    StatusCode = 404,
                    Result = null
                };
            }

            var paymentCancelInfor = await _payOS.cancelPaymentLink(paymentTransactions.OrderCode, cancellationReason);
            paymentTransactions.Status = paymentCancelInfor.status + " - " + cancellationReason;
            _unitOfWork.PaymentTransactionsRepository.Update(paymentTransactions);
            await _unitOfWork.SaveAsync();
            return new ResponseDTO()
            {
                Message = "Cancel success",
                IsSuccess = true,
                StatusCode = 200,
                Result = paymentCancelInfor.status
            };
        }
        catch (Exception e)
        {
            return new ResponseDTO()
            {
                Message = e.Message,
                IsSuccess = false,
                StatusCode = 500,
                Result = null
            };
        }

    }
}
