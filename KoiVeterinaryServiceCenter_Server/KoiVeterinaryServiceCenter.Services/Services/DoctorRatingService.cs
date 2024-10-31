using KoiVeterinaryServiceCenter.DataAccess.IRepository;
using KoiVeterinaryServiceCenter.Models.DTO;
using KoiVeterinaryServiceCenter.Models.DTO.DoctorRating;
using KoiVeterinaryServiceCenter.Models.Domain;
using KoiVeterinaryServiceCenter.Services.IServices;
using System.Security.Claims;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KoiVeterinaryServiceCenter.Services.Services
{
    public class DoctorRatingService : IDoctorRatingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
      
        public DoctorRatingService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseDTO> CreateRating(ClaimsPrincipal user, CreateDoctorRatingDTO createDoctorRatingDTO)
        {
            try
            {
                var doctorRating = new DoctorRating
                {
                    DoctorId = createDoctorRatingDTO.DoctorId,
                    Rating = createDoctorRatingDTO.Rating,
                    Feedback = createDoctorRatingDTO.Feedback
                };

                await _unitOfWork.DoctorRatingRepository.AddAsync(doctorRating);
                await _unitOfWork.SaveAsync();

                return new ResponseDTO
                {
                    Message = "Doctor rating created successfully",
                    IsSuccess = true,
                    StatusCode = 200,
                    Result = null
                };
            }
            catch (Exception e)
            {
                return new ResponseDTO
                {
                    Message = e.Message,
                    IsSuccess = false,
                    StatusCode = 500,
                    Result = null
                };
            }
        }

        public async Task<ResponseDTO> GetAllRating(
       ClaimsPrincipal user,
       string? filterOn,
       string? filterQuery,
       string? sortBy,
       bool? isAscending,
       int pageNumber,
       int pageSize)
        {
            try
            {
                // Retrieve all ratings at once
                var ratings = await _unitOfWork.DoctorRatingRepository.GetAllAsync();

                // Apply filters
                if (!string.IsNullOrEmpty(filterOn) && !string.IsNullOrEmpty(filterQuery))
                {
                    ratings = filterOn.Trim().ToLower() switch
                    {
                        "doctorid" => ratings.Where(x => x.DoctorId.ToString().Contains(filterQuery)).ToList(),
                        "feedback" => ratings.Where(x => x.Feedback.Contains(filterQuery, StringComparison.CurrentCultureIgnoreCase)).ToList(),
                        "rating" when int.TryParse(filterQuery, out var ratingValue) => ratings.Where(x => x.Rating == ratingValue).ToList(),
                        
                        _ => ratings
                    };
                }

                // Apply sorting
                if (!string.IsNullOrEmpty(sortBy))
                {
                    ratings = sortBy.Trim().ToLower() switch
                    {
                        "rating" => isAscending == true ? ratings.OrderBy(x => x.Rating).ToList() : ratings.OrderByDescending(x => x.Rating).ToList(),
                        "doctorid" => isAscending == true ? ratings.OrderBy(x => x.DoctorId).ToList() : ratings.OrderByDescending(x => x.DoctorId).ToList(),
                        "feedback" => isAscending == true ? ratings.OrderBy(x => x.Feedback).ToList() : ratings.OrderByDescending(x => x.Feedback).ToList(),
                        _ => ratings
                    };
                }

                // Apply pagination
                if (pageNumber > 0 && pageSize > 0)
                {
                    var skipResult = (pageNumber - 1) * pageSize;
                    ratings = ratings.Skip(skipResult).Take(pageSize).ToList();
                }

                // Check if ratings list is empty
                if (!ratings.Any())
                {
                    return new ResponseDTO
                    {
                        Message = "No ratings found",
                        IsSuccess = false,
                        StatusCode = 404,
                        Result = null
                    };
                }

                // Map to DTO
                var ratingDtos = ratings.Select(r => new GetDoctorRatingDTO
                {
                    DoctorRatingId = r.DoctorRatingId,
                    DoctorId = r.DoctorId,
                    Rating = r.Rating,
                    Feedback = r.Feedback,
                    AppointmentId=r.AppointmentId
                }).ToList();

                return new ResponseDTO
                {
                    Message = "Retrieved all ratings successfully",
                    IsSuccess = true,
                    StatusCode = 200,
                    Result = ratingDtos
                };
            }
            catch (Exception e)
            {
                return new ResponseDTO
                {
                    Message = e.Message,
                    IsSuccess = false,
                    StatusCode = 500,
                    Result = null
                };
            }
        }



        public async Task<ResponseDTO> GetRatingByDoctorId(Guid doctorId)
        {
            try
            {
                // Fetch ratings for the specified doctor
                var ratings = await _unitOfWork.DoctorRatingRepository.GetByDoctorIdAsync(doctorId);

                // Check for null or empty ratings
                if (ratings == null || !ratings.Any())
                {
                    return new ResponseDTO
                    {
                        Message = "No ratings found for the specified doctor",
                        IsSuccess = false,
                        StatusCode = 404,
                        Result = null
                    };
                }

                // Map ratings to DTOs
                var getDoctorRatingsDTO = ratings.Select(r => new GetDoctorRatingDTO
                {
                    DoctorRatingId = r.DoctorRatingId,
                    DoctorId = r.DoctorId,
                    Rating = r.Rating,
                    Feedback = r.Feedback,
                    AppointmentId = r.AppointmentId
                }).ToList();

                return new ResponseDTO
                {
                    Message = "Retrieved doctor ratings successfully",
                    IsSuccess = true,
                    StatusCode = 200,
                    Result = getDoctorRatingsDTO
                };
            }
            catch (Exception e)
            {
                // Log the exception (consider using a logging framework)
                return new ResponseDTO
                {
                    Message = "An error occurred while retrieving ratings: " + e.Message,
                    IsSuccess = false,
                    StatusCode = 500,
                    Result = null
                };
            }
        }


        public async Task<ResponseDTO> GetRatesByDoctorId(Guid doctorId)
        {
            try
            {
                var averageRating = await _unitOfWork.DoctorRatingRepository.GetRatesByDoctorIdAsync(doctorId);
                if (averageRating == null)
                {
                    return new ResponseDTO
                    {
                        Message = "No ratings found for the specified doctor",
                        IsSuccess = false,
                        StatusCode = 404,
                        Result = null
                    };
                }

                return new ResponseDTO
                {
                    Message = "Retrieved average rating successfully",
                    IsSuccess = true,
                    StatusCode = 200,
                    Result = averageRating
                };
            }
            catch (Exception e)
            {
                return new ResponseDTO
                {
                    Message = e.Message,
                    IsSuccess = false,
                    StatusCode = 500,
                    Result = null
                };
            }
        }

        public async Task<ResponseDTO> GetAllRates()
        {
            try
            {
                var averageRatings = await _unitOfWork.DoctorRatingRepository.GetRatesAsync();
                if (averageRatings == null)
                {
                    return new ResponseDTO
                    {
                        Message = "No average ratings found",
                        IsSuccess = false,
                        StatusCode = 404,
                        Result = null
                    };
                }

                return new ResponseDTO
                {
                    Message = "Retrieved all average ratings successfully",
                    IsSuccess = true,
                    StatusCode = 200,
                    Result = averageRatings
                };
            }
            catch (Exception e)
            {
                return new ResponseDTO
                {
                    Message = e.Message,
                    IsSuccess = false,
                    StatusCode = 500,
                    Result = null
                };
            }
        }

        public async Task<ResponseDTO> UpdateRating(ClaimsPrincipal user, UpdateDoctorRatingDTO updateDoctorRatingDTO)
        {
            try
            {
                var doctorRating = await _unitOfWork.DoctorRatingRepository.GetByIdAsync(updateDoctorRatingDTO.DoctorRatingId);
                if (doctorRating == null)
                {
                    return new ResponseDTO
                    {
                        Message = "Doctor rating not found",
                        IsSuccess = false,
                        StatusCode = 404,
                        Result = null
                    };
                }

                doctorRating.Rating = updateDoctorRatingDTO.Rating;
                doctorRating.Feedback = updateDoctorRatingDTO.Feedback;
                doctorRating.AppointmentId= updateDoctorRatingDTO.AppointmentId;

                _unitOfWork.DoctorRatingRepository.Update(doctorRating);
                await _unitOfWork.SaveAsync();

                return new ResponseDTO
                {
                    Message = "Doctor rating updated successfully",
                    IsSuccess = true,
                    StatusCode = 200,
                    Result = null
                };
            }
            catch (Exception e)
            {
                return new ResponseDTO
                {
                    Message = e.Message,
                    IsSuccess = false,
                    StatusCode = 500,
                    Result = null
                };
            }
        }

        public async Task<ResponseDTO> DeleteRating (ClaimsPrincipal user, Guid doctorRatingId)
        {
            try
            {
                var doctorRating = await _unitOfWork.DoctorRatingRepository.GetByIdAsync(doctorRatingId);
                if (doctorRating == null)
                {
                    return new ResponseDTO
                    {
                        Message = "Doctor rating not found",
                        IsSuccess = false,
                        StatusCode = 404,
                        Result = null
                    };
                }

                _unitOfWork.DoctorRatingRepository.Remove(doctorRating);
                await _unitOfWork.SaveAsync();

                return new ResponseDTO
                {
                    Message = "Doctor rating deleted successfully",
                    IsSuccess = true,
                    StatusCode = 200,
                    Result = null
                };
            }
            catch (Exception e)
            {
                return new ResponseDTO
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
