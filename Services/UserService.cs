using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class UserService
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _configuration;

    public UserService(AppDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<UserResponseDTO?> UserRegister(UserRegisterDTO registerDTO)
    {
        var normalizedEmail = registerDTO.Email.Trim().ToLower();

        var exists = await _context.Users.AnyAsync(u =>
            u.Email != null && u.Email.ToLower() == normalizedEmail);

        if (exists)
            return null;

        var user = new User
        {
            Name = registerDTO.Name,
            Email = normalizedEmail
        };

        var hasher = new PasswordHasher<User>();
        var hash = hasher.HashPassword(user, registerDTO.Password);

        user.PasswordHash = hash;
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return new UserResponseDTO
        {
            Name = user.Name,
            Email = user.Email,
            Id = user.Id
        };
    }

    public async Task<LoginResponseDTO?> UserLogin(UserLoginDTO loginDTO)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == loginDTO.Email);

        if (user == null)
            return null;

        var hasher = new PasswordHasher<User>();
        var result = hasher.VerifyHashedPassword(user, user.PasswordHash, loginDTO.Password);

        if (result == PasswordVerificationResult.Failed)
            return null;

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email)
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds
            );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        return new LoginResponseDTO
        {
            Token = tokenString,
            User = new UserResponseDTO
            {
                Name = user.Name,
                Id = user.Id,
                Email = user.Email
            }
        };
    }

    public async Task<User?> UpdateUser(int userId, string name)
    {
        var updatedUser = await _context.Users
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (updatedUser == null)
            return null;

        updatedUser.Name = name;
        await _context.SaveChangesAsync();
        return updatedUser;
    }

    public async Task<bool> UpdatePassword(int userId, string currentPassword, string newPassword)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null)
            return false;

        var hasher = new PasswordHasher<User>();
        var result = hasher.VerifyHashedPassword(user, user.PasswordHash, currentPassword);

        if (result == PasswordVerificationResult.Failed)
            return false;

        user.PasswordHash = hasher.HashPassword(user, newPassword);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<User?> DeleteUser(int userId)
    {
        var deletedUser = await _context.Users
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (deletedUser == null)
            return null;

        _context.Users.Remove(deletedUser);
        await _context.SaveChangesAsync();
        return deletedUser;
    }
}