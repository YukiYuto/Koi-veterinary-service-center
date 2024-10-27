using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using AutoMapper;
using KoiVeterinaryServiceCenter.DataAccess.IRepository;
using KoiVeterinaryServiceCenter.Models.DTO;
using KoiVeterinaryServiceCenter.Models.DTO.Transaction;
using KoiVeterinaryServiceCenter.Services.IServices;
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

        public Task<ResponseDTO> CreateTransaction(ClaimsIdentity User, CreateTransactionDTO createTransactionDTO)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDTO> GetAll(ClaimsPrincipal User, string? filterOn, string? filterQuery, string? sortBy, bool? isAscending, int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseDTO> GetById(ClaimsPrincipal User, Guid transactionId)
        {
            try
            {
                var transaction = _unitOfWork.TransactionsRepository.GetTransactionById(transactionId);

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

                GetTransactionDTO getTransactionDTO;
                try
                {
                    getTransactionDTO = _mapper.Map<GetTransactionDTO>(transaction);
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
                    Message = "Add transaction successfully",
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
