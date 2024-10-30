using KoiVeterinaryServiceCenter.Models.Domain;

namespace KoiVeterinaryServiceCenter.DataAccess.IRepository
{
    public interface ITransactionsRepository : IRepository<Transaction>
    {
        Task<Transaction> GetTransactionById(Guid transactionId);
        Task<Transaction> GetTransactionFullInfoById(Guid transactionId);
    }
}
