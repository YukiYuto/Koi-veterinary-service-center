using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiVeterinaryServiceCenter.Model.Domain
{
    public class Transaction
    {
        [Key] public Guid TransactionId { get; set; }
        public string CustomerId { get; set; }
        public Guid? AppointmentId { get; set; }
        public Guid PaymentMethodId { get; set; }
        public double Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public string TransactionStatus { get; set; }
        public DateTime CreateTime { get; set; }
        public string Status { get; set; }

        [ForeignKey("CustomerId")] public virtual ApplicationUser ApplicationUser { get; set; }
        [ForeignKey("AppointmentId")] public virtual Appointment Appointment { get; set; }
        [ForeignKey("PaymentMethodId")] public virtual PaymentMethod PaymentMethod { get; set; }
    }
}
