using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KoiVeterinaryServiceCenter.Models.Domain;

public class Transaction
{
    [Key] public Guid TransactionId { get; set; }
    public string CustomerId { get; set; } = null!;
    public Guid? AppointmentId { get; set; }
    public Guid PaymentMethodId { get; set; }
    public double Amount { get; set; }
    public DateTime TransactionDate { get; set; }
    public string TransactionStatus { get; set; } = null!;
    public DateTime CreateTime { get; set; }
    public string Status { get; set; } = null!;

    [ForeignKey("CustomerId")] public virtual ApplicationUser ApplicationUser { get; set; } = null!;
    [ForeignKey("AppointmentId")] public virtual Appointment Appointment { get; set; }
    [ForeignKey("PaymentMethodId")] public virtual PaymentMethod PaymentMethod { get; set; } = null!;
}