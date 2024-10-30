using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using KoiVeterinaryServiceCenter.DataAccess.IRepository;
using KoiVeterinaryServiceCenter.Models.Domain;
using KoiVeterinaryServiceCenter.Services.IServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace KoiVeterinaryServiceCenter.Services.Services;

public class TokenService : ITokenService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _configuration;
    private readonly IRedisService _redisService;

    public TokenService(UserManager<ApplicationUser> userManager, IConfiguration configuration, IUnitOfWork unitOfWork, IRedisService redis)
    {
        _userManager = userManager;
        _configuration = configuration;
        _redisService = redis;
    }

    /// <summary>
    /// This method for create Access token
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public async Task<string> GenerateJwtAccessTokenAsync(ApplicationUser user)
    {
        var userRoles = await _userManager.GetRolesAsync(user);

        var authClaims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, user.UserName),
        new Claim(ClaimTypes.NameIdentifier, user.Id),
        new Claim("FullName", user.FullName),  // Thêm FullName vào claims
        new Claim("Email", user.Email),        // Thêm Email vào claims
        new Claim("PhoneNumber", user.PhoneNumber), // Thêm PhoneNumber vào claims
        new Claim("Address", user.Address),    // Thêm Address vào claims
        new Claim("Country", user.Country),    // Thêm Country vào claims
        new Claim("Gender", user.Gender.ToString()), // Thêm Gender vào claims
        new Claim("AvatarUrl", user.AvatarUrl)
    };

        foreach (var role in userRoles)
        {
            authClaims.Add(new Claim(ClaimTypes.Role, role));
        }

        var authSecret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
        var signingCredentials = new SigningCredentials(authSecret, SecurityAlgorithms.HmacSha256);

        var tokenObject = new JwtSecurityToken(
            issuer: _configuration["JWT:ValidIssuer"],
            audience: _configuration["JWT:ValidAudience"],
            notBefore: DateTime.Now,
            expires: DateTime.Now.AddMinutes(60), //Expiration time is 1h
            claims: authClaims,
            signingCredentials: signingCredentials
        );

        var accessToken = new JwtSecurityTokenHandler().WriteToken(tokenObject);

        return accessToken;
    }


    /// <summary>
    /// Create Refresh Token
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public async Task<string> GenerateJwtRefreshTokenAsync(ApplicationUser user)
    {
        // Create a list of claims containing user information
        var authClaims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
        };

        // Create cryptographic objects for tokens
        var authSecret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
        var signingCredentials = new SigningCredentials(authSecret, SecurityAlgorithms.HmacSha256);

        // Create JWT token object
        var tokenObject = new JwtSecurityToken(
            issuer: _configuration["JWT:ValidIssuer"],
            audience: _configuration["JWT:ValidAudience"],
            notBefore: DateTime.Now,
            expires: DateTime.Now.AddDays(3), //Expiration time is 3 days
            claims: authClaims,
            signingCredentials: signingCredentials
        );

        // Token generation successful
        var refreshToken = new JwtSecurityTokenHandler().WriteToken(tokenObject);

        return refreshToken;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<ClaimsPrincipal> GetPrincipalFromToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]);

        try
        {
            var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _configuration["JWT:ValidIssuer"],
                ValidAudience = _configuration["JWT:ValidAudience"],
                IssuerSigningKey = new SymmetricSecurityKey(key)
            }, out SecurityToken validatedToken);

            return principal;
        }
        catch
        {
            return null;
        }
    }





    /// <summary>
    /// This method for store refresh token on redis cloud
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="refreshToken"></param>
    /// <returns></returns>
    public async Task<bool> StoreRefreshToken(string userId, string refreshToken)
    {
        string redisKey = $"userId:{userId}:refreshToken";
        var result = await _redisService.StoreString(redisKey, refreshToken);
        return true;
    }

    /// <summary>
    /// This method for get refresh token from redis cloud
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<string> RetrieveRefreshToken(string userId)
    {
        string redisKey = $"userId:{userId}:refreshToken";
        var result = await _redisService.RetrieveString(redisKey);
        return result;
    }

    /// <summary>
    /// This method for delete refresh token on redis cloud
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<bool> DeleteRefreshToken(string userId)
    {
        string redisKey = $"userId:{userId}:refreshToken";
        var result = await _redisService.DeleteString(redisKey);
        return result;
    }
}