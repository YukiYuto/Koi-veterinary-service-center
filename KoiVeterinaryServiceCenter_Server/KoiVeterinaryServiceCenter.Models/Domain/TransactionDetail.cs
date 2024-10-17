using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KoiVeterinaryServiceCenter.Models.Domain;

public class TransactionDetail : BaseEntity<string, string, int>
{
    [Key]
    public Guid TransactionDetailId { get; set; }
    public Guid TransactionId { get; set; }
    [ForeignKey("TransactionId")] public virtual Transaction Transaction { get; set; } = null!;
}