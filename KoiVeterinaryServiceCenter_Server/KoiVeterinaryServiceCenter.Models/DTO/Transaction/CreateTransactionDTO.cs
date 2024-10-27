using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiVeterinaryServiceCenter.Models.DTO.Transaction
{
    public class CreateTransactionDTO
    {
        public Guid TransactionId { get; set; }
        public string CustomerId { get; set; } = null!;
        public Guid? AppointmentId { get; set; }
        public Guid PaymentTransactionId { get; set; }
        public double Amount { get; set; }
        public string? TransactionStatus { get; set; }
        public string Status { get; set; } = null!;
    }
}
