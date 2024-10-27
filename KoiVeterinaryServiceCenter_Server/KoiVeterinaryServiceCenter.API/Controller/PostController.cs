using System.Security.Claims;
using KoiVeterinaryServiceCenter.Models.DTO;
using KoiVeterinaryServiceCenter.Models.DTO.Post;
using KoiVeterinaryServiceCenter.Services.IServices;
using KoiVeterinaryServiceCenter.Utility.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KoiVeterinaryServiceCenter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDTO>> GetAllPosts(
            [FromQuery] string? filterOn,
            [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy,
            [FromQuery] bool? isAscending,
            [FromQuery] int pageNumber,
            [FromQuery] int pageSize)
        {
            var responseDto = await _postService.GetAllPosts(User, filterOn, filterQuery, sortBy, isAscending, pageNumber, pageSize);
            return StatusCode(responseDto.StatusCode, responseDto);
        }

        [HttpPost]
      
        public async Task<ActionResult<ResponseDTO>> CreatePost([FromBody] CreatePostDTO createPostDTO)
        {
            var responseDto = await _postService.CreatePost(User, createPostDTO);
            return StatusCode(responseDto.StatusCode, responseDto);
        }

        [HttpGet("{postId:guid}")]
        public async Task<ActionResult<ResponseDTO>> GetPostById([FromRoute] Guid postId)
        {
            var responseDto = await _postService.GetPostById(postId);
            return StatusCode(responseDto.StatusCode, responseDto);
        }

        [HttpPut("{postId:guid}")]
        [Authorize(Roles = StaticUserRoles.Admin)]
        public async Task<ActionResult<ResponseDTO>> UpdatePost([FromRoute] Guid postId, [FromBody] UpdatePostDTO updatePostDTO)
        {
            updatePostDTO.Id = postId; // Assuming you have an Id property in your UpdatePostDTO
            var responseDto = await _postService.UpdatePost(User, updatePostDTO);
            return StatusCode(responseDto.StatusCode, responseDto);
        }

        [HttpDelete("{postId:guid}")]
        [Authorize(Roles = StaticUserRoles.Admin)]
        public async Task<ActionResult<ResponseDTO>> DeletePost([FromRoute] Guid postId)
        {
            var responseDto = await _postService.DeletePost(User, postId);
            return StatusCode(responseDto.StatusCode, responseDto);
        }
    }
}
