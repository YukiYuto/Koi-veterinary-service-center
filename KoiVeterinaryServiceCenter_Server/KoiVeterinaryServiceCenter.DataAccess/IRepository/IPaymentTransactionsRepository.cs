using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KoiVeterinaryServiceCenter.Model.Domain;

namespace KoiVeterinaryServiceCenter.DataAccess.IRepository
{
    public interface IPaymentTransactionsRepository : IRepository<PaymentTransactions>
    {
        void Update(PaymentTransactions transaction);
        Task<PaymentTransactions> GetById(Guid transactionId);
    }
}
