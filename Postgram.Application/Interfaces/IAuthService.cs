using Postgram.Application.DTOs.AuthDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Postgram.Application.Interfaces
{
    public interface IAuthService
    {
        public Task<AuthResponseDto> LoginAsync(LoginDto dto);
        public Task<AuthResponseDto> RegisterAsync(RegisterDto dto);
    }
}
