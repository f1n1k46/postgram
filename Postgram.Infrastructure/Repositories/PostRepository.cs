using Microsoft.EntityFrameworkCore;
using Postgram.Infrastructure.Data;
using Postgram.Application.DTOs.PostDto;
using Postgram.Domain.Models;
using Postgram.Application.Interfaces;

namespace Postgram.Infrastructure.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly PostgramDbContext _context;
        private readonly IUserRepository _userRepository;
        public PostRepository(PostgramDbContext context, IUserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;
        }

        public async Task<Post> CreatePostAsync(CreatePostDto dto, int userId)
        {
            var post = new Post
            {
                UserId = userId,
                Title = dto.Title,
                Description = dto.Text,
            };

            _context.Posts.Add(post);
            await _context.SaveChangesAsync();

            return post;
        }

        public async Task<bool> DeletePostAsync(int postId)
        {
            var post = await _context.Posts.FindAsync(postId);
            
            if (post == null)
            {
                throw new KeyNotFoundException();
            }

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Post> UpdatePostAsync(int postId, UpdatePostDto dto)
        {
            var post = await _context.Posts.FindAsync(postId);

            if (post == null)
            {
                throw new KeyNotFoundException();
            }

            post.Title = dto.Title;
            post.Description = dto.Text;

            await _context.SaveChangesAsync();

            return post;
        }

        public async Task<Post?> GetPostByIdAsync(int postId)
        {
            return await _context.Posts.FindAsync(postId);
        }

        public async Task<List<Post>> GetAllPostsAsync()
        {
            return await _context.Posts.ToListAsync();
        }
    }
}