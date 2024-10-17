using System.ComponentModel.DataAnnotations;

namespace KoiVeterinaryServiceCenter.Models.Domain;

public class PaymentMethod : BaseEntity<string, string, int>
{
    [Key] public Guid PaymentMethodId { get; set; }
    public string MethodName { get; set; } = null!;
}