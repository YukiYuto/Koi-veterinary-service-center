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
using KoiVeterinaryServiceCenter.Models.Domain;
using Transaction = KoiVeterinaryServiceCenter.Models.Domain.Transaction;

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
        try
        {
            var appointment = await _unitOfWork.AppointmentRepository.GetAppointmentByAppmointNumer(createPaymentLink.AppointmentNumber);
            if (appointment is null)
            {
                return new ResponseDTO()
                {
                    Message = "Appointment is not exist",
                    IsSuccess = false,
                    StatusCode = 404,
                    Result = null
                };
            }
            var totalPrice = Convert.ToInt32(appointment.TotalAmount);
            var service = await _unitOfWork.ServiceRepository.GetServiceById(appointment.ServiceId);
            var items = new List<ItemData>()
            {
                new ItemData( name: service.ServiceName, quantity: 1, price: totalPrice)
            };


            var paymentData = new PaymentData(
                orderCode: createPaymentLink.AppointmentNumber,
                amount: totalPrice,
                description: "",
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
                AppointmentNumber = createPaymentLink.AppointmentNumber,
                Amount = result.amount,
                Description = result.description.Trim(),
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
                Result = new
                {
                    result,
                    PaymentTransactionId = paymentTransactions.PaymentTransactionId
                }
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

            var paymentStatus = _payOS.getPaymentLinkInformation(paymentTransactions.AppointmentNumber);

            if (paymentStatus != null)
            {
                paymentTransactions.Status = paymentStatus.Result.status;
            }

            _unitOfWork.PaymentTransactionsRepository.Update(paymentTransactions);
            await _unitOfWork.SaveAsync();

            if(paymentTransactions.Status.Equals(StaticPayment.paymentStatusSucess))
            {
                var appointment = await _unitOfWork.AppointmentRepository.GetAppointmentByAppmointNumer(paymentTransactions.AppointmentNumber);
                Transaction transaction = new Transaction()
                {
                    CustomerId = appointment.CustomerId,
                    AppointmentId = appointment.AppointmentId,
                    PaymentTransactionId = paymentTransactionId,
                    Amount = paymentTransactions.Amount,
                    TransactionDateTime = DateTime.Now
                };

                await _unitOfWork.TransactionsRepository.AddAsync(transaction);
                await _unitOfWork.SaveAsync();
            }
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

            var paymentCancelInfor = await _payOS.cancelPaymentLink(paymentTransactions.AppointmentNumber, cancellationReason);
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
