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
using KoiVeterinaryServiceCenter.Models.DTO.Pool;
using KoiVeterinaryServiceCenter.Services.IServices;
using Microsoft.IdentityModel.Tokens;

namespace KoiVeterinaryServiceCenter.Services.Services
{
    public class PoolService : IPoolService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PoolService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseDTO> CreatePool(ClaimsPrincipal User, CreatePoolDTO createPoolDTO)
        {
            /*try
            {
                Pool pool = new Pool()
                {
                    Name = createPoolDTO.Name,
                    CustomerId = createPoolDTO.CustomerId,
                    Size = createPoolDTO.Size
                };

                await _unitOfWork.PoolRepository.AddAsync(pool);
                await _unitOfWork.SaveAsync();

                return new ResponseDTO()
                {
                    Message = "Add pool successfully",
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
            }*/
            return null;
        }

        public Task<ResponseDTO> DeletePoolById(ClaimsPrincipal User, Guid poolId)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseDTO> GetAll(ClaimsPrincipal User, string? filterOn, string? filterQuery,
            string? sortBy, bool? isAscending, int pageNumber, int pageSize)
        {
            try
            {
                List<Pool> pools = _unitOfWork.PoolRepository.GetAllAsync()
                    .GetAwaiter().GetResult().ToList();
                if (pools.IsNullOrEmpty())
                {
                    return new ResponseDTO()
                    {
                        Message = "Not has any pool",
                        IsSuccess = false,
                        StatusCode = 404,
                        Result = null
                    };
                }

                if (!string.IsNullOrEmpty(filterOn) && !string.IsNullOrEmpty(filterQuery))
                {
                    switch (filterOn.Trim().ToLower())
                    {
                        case "name":
                            pools = pools.Where(x => x.Name == filterQuery).ToList();
                            break;
                        default:
                            break;
                    }
                }

                if (!string.IsNullOrEmpty(sortBy))
                {
                    pools = sortBy.Trim().ToLower() switch
                    {
                        "name" => isAscending == true
                            ? pools.OrderBy(x => x.Name).ToList()
                            : pools.OrderByDescending(x => x.Name).ToList(),

                        _ => pools
                    };
                }

                ;

                if (pageNumber > 0 && pageSize > 0)
                {
                    var skipResult = (pageNumber - 1) * pageSize;
                    pools = pools.Skip(skipResult).Take(pageSize).ToList();
                }

                List<GetPoolDTO> poolList = new List<GetPoolDTO>();
                try
                {
                    poolList = _mapper.Map<List<GetPoolDTO>>(pools);
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
                    Message = "Get all pool successfully",
                    IsSuccess = true,
                    StatusCode = 200,
                    Result = poolList
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

        public async Task<ResponseDTO> GetPoolByCustomerId(ClaimsPrincipal User, string customerId)
        {
            try
            {
                var pool = await _unitOfWork.PoolRepository.GetAllAsync(x => x.CustomerId == customerId);
                if (pool is null)
                {
                    return new ResponseDTO()
                    {
                        Message = "Cannot found pool",
                        IsSuccess = false,
                        StatusCode = 404,
                        Result = null
                    };
                }

                List<GetPoolDTO> getPoolDTO;
                try
                {
                    getPoolDTO = _mapper.Map<List<GetPoolDTO>>(pool);
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
                    Message = "Get pool successfully",
                    IsSuccess = true,
                    StatusCode = 200,
                    Result = getPoolDTO
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
        public async Task<ResponseDTO> UpdatePool(ClaimsPrincipal User, UpdatePoolDTO updatePoolDTO)
        {
            /*try
            public async Task<ResponseDTO> GetPoolById(ClaimsPrincipal User, Guid poolId)
            {
                try
                {
                    var pool = await _unitOfWork.PoolRepository.GetById(poolId);
                    if (pool is null)
                    {
                        return new ResponseDTO()
                        {
                            Message = "Cannot found pool",
                            IsSuccess = false,
                            StatusCode = 404,
                            Result = null
                        };
                    }

                    GetPoolFullInfo getPoolFullInfo;
                    try
                    {
                        getPoolFullInfo = _mapper.Map<GetPoolFullInfo>(pool);
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
                        Message = "Get pool successfully",
                        IsSuccess = true,
                        StatusCode = 200,
                        Result = getPoolFullInfo
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

            public async Task<ResponseDTO> UpdatePool(ClaimsPrincipal User, UpdatePoolDTO updatePoolDTO)
            {
                try
                {
                    var pool = await _unitOfWork.PoolRepository.GetById(updatePoolDTO.PoolId);
                    if (pool is null)
                    {
                        return new ResponseDTO()
                        {
                            Message = "Cannot found pool",
                            IsSuccess = false,
                            StatusCode = 404,
                            Result = null
                        };
                    }
                    pool.Size = updatePoolDTO.Size;
                    _unitOfWork.PoolRepository.Update(pool);
                    await _unitOfWork.SaveAsync();

                    return new ResponseDTO()
                    {
                        Message = "Update pool successfully",
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
        }*/
            return null;
        }
    }
