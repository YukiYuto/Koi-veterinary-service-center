using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KoiVeterinaryServiceCenter.Models.Domain;

namespace KoiVeterinaryServiceCenter.Model.Domain
{
    public class PaymentTransactions
    {
        [Key]
        public Guid PaymentTransactionId { get; set; }
        public long AppointmentNumber { get; set; }
        [ForeignKey("AppointmentNumber")] public virtual Appointment Appointment { get; set; }
        public int Amount { get; set; }
        public string Description { get; set; }
        public string CancelUrl { get; set; }
        public string ReturnUrl { get; set; }
        public long? ExpiredAt { get; set; }
        public string? Signature { get; set; }
        public DateTime? CreatedAt { get; set; }
        
        public string? Status { get; set; }
        public string? Reason { get; set; }
        
    }
}
