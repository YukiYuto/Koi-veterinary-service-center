using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KoiVeterinaryServiceCenter.Model.Domain;

namespace KoiVeterinaryServiceCenter.Models.Domain;

public class Transaction
{
    [Key] public Guid TransactionId { get; set; }
    public string CustomerId { get; set; } = null!;
    public Guid? AppointmentId { get; set; }
    public Guid PaymentTransactionId { get; set; }
    public double Amount { get; set; }
    public DateTime TransactionDateTime { get; set; }
    public string TransactionMethod { get; set; } = null!;

    [ForeignKey("CustomerId")] public virtual ApplicationUser ApplicationUser { get; set; } = null!;
    [ForeignKey("AppointmentId")] public virtual Appointment Appointment { get; set; }
    [ForeignKey("PaymentTransactionId")] public virtual PaymentTransactions PaymentTransactions { get; set; }
}