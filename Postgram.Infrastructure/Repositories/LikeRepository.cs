using Microsoft.EntityFrameworkCore;
using Postgram.Infrastructure.Data;
using Postgram.Application.DTOs.LikeDto;
using Postgram.Domain.Models;
using Postgram.Application.Interfaces;

namespace Postgram.Infrastructure.Repositories
{
    public class LikeRepository : ILikeRepository
    {
        private readonly PostgramDbContext _dbContext;

        public LikeRepository(PostgramDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> LikePostAsync(LikeDto dto, int userId)
        {
            if (await _dbContext.Likes.AnyAsync(l =>
                l.UserId == userId &&
                l.PostId == dto.PostId))
            {
                return false;
            }

            var like = new Like
            { 
                UserId = userId,
                PostId = dto.PostId
            };

            await _dbContext.Likes.AddAsync(like);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UnlikePostAsync(LikeDto dto, int userId)
        {
            var like = await _dbContext.Likes.FirstOrDefaultAsync(l =>
            l.UserId == userId && l.PostId == dto.PostId);
            
            if (like == null)
            {
                return false;
            }

            _dbContext.Likes.Remove(like);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}