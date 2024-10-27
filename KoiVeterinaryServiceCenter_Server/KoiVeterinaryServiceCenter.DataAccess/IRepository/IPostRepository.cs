using KoiVeterinaryServiceCenter.Models.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KoiVeterinaryServiceCenter.DataAccess.IRepository
{
    public interface IPostRepository : IRepository<Post>
    {
        Task<Post?> GetByIdAsync(Guid postId);
        Task<List<Post>> GetAllPostsAsync();
        Task<List<Post>> GetPostsByTitleAsync(string title);
        void Update(Post post);
        void UpdateRange(IEnumerable<Post> posts);
    }
}
