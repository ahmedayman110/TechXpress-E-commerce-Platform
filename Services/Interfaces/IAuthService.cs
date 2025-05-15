using TechXpress.DTOs;
using TechXpress.Models;

namespace TechXpress.Services.Interfaces
{
    public interface IAuthService
    {
        Task<User> RegisterAsync(RegisterDTO registerDTO);
        Task<string> LoginAsync(LoginDTO loginDTO);
        
    }
}
