using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiVeterinaryServiceCenter.Models.DTO.Transaction
{
    public class GetTransactionDTO
    {
        public Guid TransactionId { get; set; }
        public string CustomerId { get; set; }
        public Guid? AppointmentId { get; set; }
        public Guid PaymentTransactionId { get; set; }
        public double Amount { get; set; }
        public DateTime TransactionDateTime { get; set; }
    }
}
