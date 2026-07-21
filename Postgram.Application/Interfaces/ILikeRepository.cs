using Postgram.Application.DTOs.LikeDto;

namespace Postgram.Application.Interfaces
{
    public interface ILikeRepository
    {
        public Task<bool> LikePostAsync(LikeDto dto, int userId);
        public Task<bool> UnlikePostAsync(LikeDto dto, int userId);
    }
}
