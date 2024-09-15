using KoiVeterinaryServiceCenter.DataAccess.IRepository;
using KoiVeterinaryServiceCenter.Model.Domain;

namespace KoiVeterinaryServiceCenter.DataAccess.IRepository
{

    public interface IRefreshTokenRepository : IRepository<RefreshTokens>
    {
        Task<RefreshTokens> GetTokenByUserIdAsync(string userId);
        Task AddTokenAsync(RefreshTokens token);
        Task RemoveTokenAsync(RefreshTokens token);
    }
}