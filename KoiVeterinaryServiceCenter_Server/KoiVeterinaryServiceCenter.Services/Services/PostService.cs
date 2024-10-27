using KoiVeterinaryServiceCenter.DataAccess.IRepository;
using KoiVeterinaryServiceCenter.Models.DTO;
using KoiVeterinaryServiceCenter.Models.DTO.Post; // Ensure you have DTOs for Post
using KoiVeterinaryServiceCenter.Models.Domain;
using KoiVeterinaryServiceCenter.Services.IServices;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Linq;

namespace KoiVeterinaryServiceCenter.Services.Services
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PostService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseDTO> CreatePost(ClaimsPrincipal user, CreatePostDTO createPostDTO)
        {
            try
            {
                var post = new Post
                {
                    Title = createPostDTO.Title,
                    Content = createPostDTO.Content,
                    PostUrl = createPostDTO.PostUrl,
                    PostId = Guid.NewGuid() // Generate a new Guid for the post
                };

                await _unitOfWork.PostRepository.AddAsync(post);
                await _unitOfWork.SaveAsync();

                return new ResponseDTO
                {
                    Message = "Post created successfully",
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

        public async Task<ResponseDTO> GetAllPosts(
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
                // Retrieve all posts
                var posts = await _unitOfWork.PostRepository.GetAllAsync();

                // Apply filters
                if (!string.IsNullOrEmpty(filterOn) && !string.IsNullOrEmpty(filterQuery))
                {
                    posts = filterOn.Trim().ToLower() switch
                    {
                        "title" => posts.Where(x => x.Title.Contains(filterQuery, StringComparison.CurrentCultureIgnoreCase)).ToList(),
                        "content" => posts.Where(x => x.Content.Contains(filterQuery, StringComparison.CurrentCultureIgnoreCase)).ToList(),
                        _ => posts
                    };
                }

                // Apply sorting
                if (!string.IsNullOrEmpty(sortBy))
                {
                    posts = sortBy.Trim().ToLower() switch
                    {
                        "title" => isAscending == true ? posts.OrderBy(x => x.Title).ToList() : posts.OrderByDescending(x => x.Title).ToList(),
                        "postid" => isAscending == true ? posts.OrderBy(x => x.PostId).ToList() : posts.OrderByDescending(x => x.PostId).ToList(),
                        _ => posts
                    };
                }

                // Apply pagination
                if (pageNumber > 0 && pageSize > 0)
                {
                    var skipResult = (pageNumber - 1) * pageSize;
                    posts = posts.Skip(skipResult).Take(pageSize).ToList();
                }

                if (!posts.Any())
                {
                    return new ResponseDTO
                    {
                        Message = "No posts found",
                        IsSuccess = false,
                        StatusCode = 404,
                        Result = null
                    };
                }

                // Map to DTOs (Assuming you have a DTO for getting posts)
                var postDtos = posts.Select(p => new GetPostDTO
                {
                    PostId = p.PostId,
                    Title = p.Title,
                    Content = p.Content,
                    PostUrl = p.PostUrl
                }).ToList();

                return new ResponseDTO
                {
                    Message = "Retrieved all posts successfully",
                    IsSuccess = true,
                    StatusCode = 200,
                    Result = postDtos
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

        public async Task<ResponseDTO> GetPostById(Guid postId)
        {
            try
            {
                var post = await _unitOfWork.PostRepository.GetByIdAsync(postId);
                if (post == null)
                {
                    return new ResponseDTO
                    {
                        Message = "Post not found",
                        IsSuccess = false,
                        StatusCode = 404,
                        Result = null
                    };
                }

                var postDto = new GetPostDTO
                {
                    PostId = post.PostId,
                    Title = post.Title,
                    Content = post.Content,
                    PostUrl = post.PostUrl
                };

                return new ResponseDTO
                {
                    Message = "Retrieved post successfully",
                    IsSuccess = true,
                    StatusCode = 200,
                    Result = postDto
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

        public async Task<ResponseDTO> UpdatePost(ClaimsPrincipal user, UpdatePostDTO updatePostDTO)
        {
            try
            {
                var post = await _unitOfWork.PostRepository.GetByIdAsync(updatePostDTO.Id);
                if (post == null)
                {
                    return new ResponseDTO
                    {
                        Message = "Post not found",
                        IsSuccess = false,
                        StatusCode = 404,
                        Result = null
                    };
                }

                post.Title = updatePostDTO.Title;
                post.Content = updatePostDTO.Content;
                post.PostUrl = updatePostDTO.PostUrl;

                _unitOfWork.PostRepository.Update(post);
                await _unitOfWork.SaveAsync();

                return new ResponseDTO
                {
                    Message = "Post updated successfully",
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

        public async Task<ResponseDTO> DeletePost(ClaimsPrincipal user, Guid postId)
        {
            try
            {
                var post = await _unitOfWork.PostRepository.GetByIdAsync(postId);
                if (post == null)
                {
                    return new ResponseDTO
                    {
                        Message = "Post not found",
                        IsSuccess = false,
                        StatusCode = 404,
                        Result = null
                    };
                }

                _unitOfWork.PostRepository.Remove(post);
                await _unitOfWork.SaveAsync();

                return new ResponseDTO
                {
                    Message = "Post deleted successfully",
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
