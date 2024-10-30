using KoiVeterinaryServiceCenter.DataAccess.Context;
using KoiVeterinaryServiceCenter.DataAccess.IRepository;
using KoiVeterinaryServiceCenter.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KoiVeterinaryServiceCenter.DataAccess.Repository
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        private readonly ApplicationDbContext _context;

        public PostRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Post?> GetByIdAsync(Guid postId)
        {
            return await _context.Posts
                .FirstOrDefaultAsync(p => p.PostId == postId);
        }

        public async Task<List<Post>> GetAllPostsAsync()
        {
            return await _context.Posts.ToListAsync();
        }

        public async Task<List<Post>> GetPostsByTitleAsync(string title)
        {
            return await _context.Posts
                .Where(p => p.Title != null && p.Title.Contains(title))
                .ToListAsync();
        }

        public void Update(Post post)
        {
            _context.Posts.Update(post);
        }

        public void UpdateRange(IEnumerable<Post> posts)
        {
            _context.Posts.UpdateRange(posts);
        }
    }
}
