using System.Security.Claims;
using KoiVeterinaryServiceCenter.Model.Domain;

namespace KoiVeterinaryServiceCenter.Services.IServices;

public interface ITokenService
{
    Task<string> GenerateJwtAccessTokenAsync(ApplicationUser user);
    Task<string> GenerateJwtRefreshTokenAsync(ApplicationUser user);
    Task<ClaimsPrincipal> GetPrincipalFromToken(string token);
    Task<bool> StoreRefreshToken(string userId, string refreshToken);
    Task<string> RetrieveRefreshToken(string userId);
    Task<bool> DeleteRefreshToken(string userId);
}