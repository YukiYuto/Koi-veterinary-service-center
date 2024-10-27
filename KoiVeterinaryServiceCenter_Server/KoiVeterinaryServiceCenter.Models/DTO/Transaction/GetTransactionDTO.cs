using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiVeterinaryServiceCenter.Models.DTO.Transaction
{
    public class GetTransactionDTO
    {
        public Guid transactionId { get; set; }
        public Guid CustomerId { get; set; }
        public Guid AppointmentId { get; set; }
        public Guid PayementTransactionId { get; set; }
        public double Amount { get; set; }
        public DateOnly TrasactionDate { get; set; }
        public string TransactionStatus { get; set; }
        public TimeSpan CreateTime { get; set; }
        public string Status { get; set; }
    }
}
