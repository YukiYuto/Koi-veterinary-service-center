using KoiVeterinaryServiceCenter.Models.DTO;
using KoiVeterinaryServiceCenter.Models.DTO.Post; // Ensure you have a DTO for Post
using System.Security.Claims;
using System.Threading.Tasks;

namespace KoiVeterinaryServiceCenter.Services.IServices
{
    public interface IPostService
    {
        Task<ResponseDTO> GetAllPosts(
            ClaimsPrincipal user,
            string? filterOn,
            string? filterQuery,
            string? sortBy,
            bool? isAscending,
            int pageNumber,
            int pageSize
        );

        Task<ResponseDTO> GetPostById(Guid postId); // Get a single post by ID
        Task<ResponseDTO> CreatePost(ClaimsPrincipal user, CreatePostDTO createPostDTO); // Create a new post
        Task<ResponseDTO> UpdatePost(ClaimsPrincipal user, UpdatePostDTO updatePostDTO); // Update an existing post
        Task<ResponseDTO> DeletePost(ClaimsPrincipal user, Guid postId); // Delete a post by ID
    }
}
