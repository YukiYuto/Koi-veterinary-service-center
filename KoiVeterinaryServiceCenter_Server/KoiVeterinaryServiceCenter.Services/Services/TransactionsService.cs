using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FirebaseAdmin.Messaging;
using KoiVeterinaryServiceCenter.DataAccess.IRepository;
using KoiVeterinaryServiceCenter.Models.Domain;
using KoiVeterinaryServiceCenter.Models.DTO;
using KoiVeterinaryServiceCenter.Models.DTO.Transaction;
using KoiVeterinaryServiceCenter.Services.IServices;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;

namespace KoiVeterinaryServiceCenter.Services.Services
{
    public class TransactionsService : ITransactionsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TransactionsService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseDTO> CreateTransaction(ClaimsPrincipal User, CreateTransactionDTO createTransactionDTO)
        {
            try
            {
                Transaction transaction = new Transaction()
                {
                    CustomerId = createTransactionDTO.CustomerId,
                    AppointmentId = createTransactionDTO.AppointmentId,
                    PaymentTransactionId = createTransactionDTO.PaymentTransactionId,
                    Amount = createTransactionDTO.Amount,
                    TransactionDateTime = DateTime.Now,
                    TransactionStatus = createTransactionDTO.TransactionStatus,
                    Status = createTransactionDTO.Status
                };

                await _unitOfWork.TransactionsRepository.AddAsync(transaction);
                await _unitOfWork.SaveAsync();

                return new ResponseDTO
                {
                    Message = "Add transaction successfully",
                    IsSuccess = true,
                    StatusCode = 200,
                    Result = null
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

        public async Task<ResponseDTO> GetAll(ClaimsPrincipal User, string? filterOn, string? filterQuery, string? sortBy, bool? isAscending, int pageNumber, int pageSize)
        {
            try
            {
                List<Transaction> transactions = _unitOfWork.TransactionsRepository.GetAllAsync()
                    .GetAwaiter().GetResult().ToList();
                if (transactions is null)
                {
                    return new ResponseDTO()
                    {
                        Message = "Not have any transaction",
                        IsSuccess = false,
                        StatusCode = 404,
                        Result = null
                    };
                }

                if (!string.IsNullOrEmpty(filterOn) && !string.IsNullOrEmpty(filterQuery))
                {
                    switch (filterOn.Trim().ToLower())
                    {
                        case "amount":
                            if (double.TryParse(filterQuery, out double amount))
                            {
                                transactions = transactions.Where(x => x.Amount >= amount).ToList();
                            }
                            break;

                        case "transactiondatetime":
                            if (DateTime.TryParse(filterQuery, out DateTime transactionDateTime))
                            {
                                transactions = transactions.Where(x => x.TransactionDateTime == transactionDateTime).ToList();
                            }
                            break;
                        case "transactionstatus":
                            transactions = transactions.Where(x => x.TransactionStatus == filterQuery).ToList();
                            break;
                        default:
                            break;
                    }
                }

                if (!string.IsNullOrEmpty(sortBy))
                {
                    transactions = sortBy.Trim().ToLower() switch
                    {
                        "amount" => isAscending == true
                        ? transactions.OrderBy(tr => tr.Amount).ToList()
                        : transactions.OrderByDescending(or => or.Amount).ToList(),

                        "transactiondatetime" => isAscending == true
                        ? transactions.OrderBy(tr => tr.TransactionDateTime).ToList()
                        : transactions.OrderByDescending(tr => tr.TransactionDateTime).ToList(),

                        _ => transactions
                    };
                }

                if (pageNumber > 0 && pageSize > 0)
                {
                    var skipResult = (pageNumber - 1) * pageSize;
                    transactions = transactions.Skip(skipResult).Take(pageSize).ToList();
                }

                if (transactions.IsNullOrEmpty())
                {
                    return new ResponseDTO()
                    {
                        Message = "Cannot found transactions",
                        IsSuccess = false,
                        StatusCode = 404,
                        Result = null
                    };
                }

                List<GetTransactionDTO> transactionsList = new List<GetTransactionDTO>();
                try
                {
                    transactionsList = _mapper.Map<List<GetTransactionDTO>>(transactions);
                }
                catch (AutoMapperMappingException ex)
                {
                    return new ResponseDTO()
                    {
                        Message = ex.Message,
                        IsSuccess = false,
                        StatusCode = 500,
                        Result = null
                    };
                }
                return new ResponseDTO()
                {
                    Message = "Get all transactions successfully",
                    IsSuccess = true,
                    StatusCode = 200,
                    Result = transactionsList
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

        public async Task<ResponseDTO> GetById(ClaimsPrincipal User, Guid transactionId)
        {
            try
            {
                var transaction = await _unitOfWork.TransactionsRepository.GetTransactionFullInfoById(transactionId);

                if (transaction is null)
                {
                    return new ResponseDTO()
                    {
                        Message = "Transaction is not exist",
                        IsSuccess = false,
                        StatusCode = 404,
                        Result = null
                    };
                }

                GetFullInforTransactionDTO getTransactionDTO;
                try
                {
                    getTransactionDTO = _mapper.Map<GetFullInforTransactionDTO>(transaction);
                }
                catch (AutoMapperMappingException e)
                {
                    return new ResponseDTO()
                    {
                        Message = e.Message,
                        IsSuccess = false,
                        StatusCode = 500,
                        Result = null
                    };
                }

                return new ResponseDTO()
                {
                    Message = "Get transaction successfully",
                    IsSuccess = true,
                    StatusCode = 200,
                    Result = getTransactionDTO
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
}
