using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KoiVeterinaryServiceCenter.DataAccess.Context;
using KoiVeterinaryServiceCenter.DataAccess.IRepository;
using KoiVeterinaryServiceCenter.Model.Domain;
using Microsoft.EntityFrameworkCore;

namespace KoiVeterinaryServiceCenter.DataAccess.Repository
{
    public class PaymentTransactionsRepository : Repository<PaymentTransactions>, IPaymentTransactionsRepository
    {
        private readonly ApplicationDbContext _context;
        public PaymentTransactionsRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<PaymentTransactions> GetById(Guid transactionId)
        {
            return await _context.PaymentTransactions.FirstOrDefaultAsync(x => x.PaymentTransactionId == transactionId);
        }

        public void Update(PaymentTransactions transaction)
        {
            _context.PaymentTransactions.Update(transaction);
        }
    }
}
