using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using KoiVeterinaryServiceCenter.DataAccess.IRepository;
using KoiVeterinaryServiceCenter.Models.DTO;
using KoiVeterinaryServiceCenter.Models.DTO.DashBoard;
using KoiVeterinaryServiceCenter.Services.IServices;
using Microsoft.IdentityModel.Tokens;

namespace KoiVeterinaryServiceCenter.Services.Services
{
    public class DashBoardService : IDashBoardService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DashBoardService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseDTO> RevenuaOfMonthList(ClaimsPrincipal User, int month, int year)
        {
            try
            {
                if (month > 12 && month < 1)
                {
                    return new ResponseDTO()
                    {
                        Message = "Format of month not correct",
                        IsSuccess = false,
                        StatusCode = 400,
                        Result = null
                    };
                }
                else if (year < 2000)
                {
                    return new ResponseDTO()
                    {
                        Message = "Format of year not correct, please input smailler 2000",
                        IsSuccess = false,
                        StatusCode = 400,
                        Result = null
                    };
                }
                var revenueList = _unitOfWork.TransactionsRepository.GetAllAsync()
                    .GetAwaiter().GetResult().Where(x => x.TransactionDateTime.Month == month && x.TransactionDateTime.Year == year).ToList();
                if (revenueList.IsNullOrEmpty())
                {
                    return new ResponseDTO()
                    {
                        Message = "This month has not any transaction",
                        IsSuccess = false,
                        StatusCode = 404,
                        Result = null
                    };
                }

                List<GetRevenueOfMonthDTO> getRevenueOfMonthDTO;
                try
                {
                    getRevenueOfMonthDTO = _mapper.Map<List<GetRevenueOfMonthDTO>>(revenueList);
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
                    Message = "Get revenue list successfully",
                    IsSuccess = true,
                    StatusCode = 200,
                    Result = revenueList
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

        public async Task<ResponseDTO> TotalRevenueOfMonth(ClaimsPrincipal User, int month, int year)
        {
            try
            {
                double total = 0;
                total = _unitOfWork.TransactionsRepository.GetAllAsync()
                    .GetAwaiter().GetResult().Where(x => x.TransactionDateTime.Month == DateTime.Now.Month).Sum(x => x.Amount);
                return new ResponseDTO()
                {
                    Message = "Get revenue this month successfully",
                    IsSuccess = true,
                    StatusCode = 200,
                    Result = total
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

        public async Task<ResponseDTO> TotalRevenueOfDay(ClaimsPrincipal User, DateOnly date)
        {
            try
            {
                double total = 0;
                total = _unitOfWork.TransactionsRepository.GetAllAsync()
                .GetAwaiter().GetResult().Where(x => x.TransactionDateTime.Day == date.Day).Sum(x => x.Amount);

                return new ResponseDTO()
                {
                    Message = "Get revenue today successfully",
                    IsSuccess = true,
                    StatusCode = 200,
                    Result = total
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
