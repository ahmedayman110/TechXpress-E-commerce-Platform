using AutoMapper;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using TechXpress.Data;
using TechXpress.DTOs;
using TechXpress.Models;
using TechXpress.Repositories.Interfaces;
using TechXpress.Services.Interfaces;

public class AuthService : IAuthService
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    private readonly JwtSettings _jwtSettings;
    private readonly AppDbContext _context;

    public AuthService(IMapper mapper, IUserRepository userRepository, AppDbContext context, IOptions<JwtSettings> jwtSettings)
    {
        _mapper = mapper;
        _userRepository = userRepository;
        _context = context;
        _jwtSettings = jwtSettings.Value;
    }

    public async Task<User> RegisterAsync(RegisterDTO registerDTO)
    {
        var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.EmailAddress == registerDTO.Email);
        if (existingUser != null)
        {
            throw new Exception("User with this email already exists");
        }

        var (passwordHash, passwordSalt) = HashPassword(registerDTO.Password);

        var user = new User
        {
            Name = registerDTO.Name,
            EmailAddress = registerDTO.Email,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            Role = User.UserRole.Customer
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return user;
    }

    private (string passwordHash, string passwordSalt) HashPassword(string password)
    {
        using (var hmac = new HMACSHA512())
        {
            var passwordSalt = Convert.ToBase64String(hmac.Key);
            var passwordHash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(password)));
            return (passwordHash, passwordSalt);
        }
    }

    public async Task<string> LoginAsync(LoginDTO loginDTO)
    {
        var user = await _userRepository.GetUserByEmailAsync(loginDTO.Email);
        if (user == null || !VerifyPasswordHash(loginDTO.Password, user.PasswordHash, user.PasswordSalt))
        {
            throw new UnauthorizedAccessException("Invalid credentials");
        }

        var token = GenerateJwtToken(user);
        return token;
    }

    private string GenerateJwtToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.EmailAddress),
            new Claim(ClaimTypes.Role, user.Role.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(60), 
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private bool VerifyPasswordHash(string password, string storedHash, string storedSalt)
    {
        using (var hmac = new HMACSHA512(Convert.FromBase64String(storedSalt)))
        {
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            var computedHashString = Convert.ToBase64String(computedHash);
            return computedHashString == storedHash;
        }
    }
}
