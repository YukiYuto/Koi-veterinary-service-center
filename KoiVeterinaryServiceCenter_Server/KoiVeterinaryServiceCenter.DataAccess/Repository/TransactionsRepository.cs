using KoiVeterinaryServiceCenter.DataAccess.Context;
using KoiVeterinaryServiceCenter.DataAccess.IRepository;
using KoiVeterinaryServiceCenter.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace KoiVeterinaryServiceCenter.DataAccess.Repository
{
    public class TransactionsRepository : Repository<Transaction>, ITransactionsRepository
    {
        private readonly ApplicationDbContext _context;
        public TransactionsRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Transaction> GetTransactionById(Guid transactionId)
        {
            return await _context.Transactions.FirstOrDefaultAsync(x => x.TransactionId == transactionId);
        }

        public async Task<Transaction> GetTransactionFullInfoById(Guid transactionId)
        {
            return await _context.Transactions
                .Include(tr => tr.Appointment)
                .ThenInclude(ap => ap.Service)
                .Include(tr => tr.Appointment)
                .ThenInclude(ap => ap.Slot)
                .ThenInclude(sl => sl.DoctorSchedules)
                .ThenInclude(ds => ds.Doctor)
                .ThenInclude(d => d.ApplicationUser)
                .FirstOrDefaultAsync(tr => tr.TransactionId == transactionId);
        }
    }
}
