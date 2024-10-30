using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using KoiVeterinaryServiceCenter.DataAccess.IRepository;
using KoiVeterinaryServiceCenter.Models.Domain;
using KoiVeterinaryServiceCenter.Models.DTO;
using KoiVeterinaryServiceCenter.Models.DTO.Pool;
using KoiVeterinaryServiceCenter.Services.IServices;

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
            try
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
            }
        }

        public void DeletePoolById(ClaimsPrincipal User, Guid poolId)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDTO> GetAll(ClaimsPrincipal User, string? filterOn, string? filterQuery, string? sortBy, bool? isAscending, int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseDTO> GetPoolById(ClaimsPrincipal User, Guid poolId)
        {
            try
            {
                var pool = await _unitOfWork.PoolRepository.GetById(poolId);
                if (pool is null)
                {
                    return new ResponseDTO()
                    {
                        Message = "Get pool successfully",
                        IsSuccess = true,
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

        public void UpdatePool(ClaimsPrincipal User, UpdatePoolDTO updatePoolDTO)
        {
            throw new NotImplementedException();
        }
    }
}
