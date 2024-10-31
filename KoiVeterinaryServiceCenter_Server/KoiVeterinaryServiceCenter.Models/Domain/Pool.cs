using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KoiVeterinaryServiceCenter.Models.Domain;

public class Pool : BaseEntity<string, string, int>
{
    [Key]public Guid PoolId  { get; set; }
    public string? Name { get; set; }
    public string CustomerId { get; set; } = null!;
    public string Size { get; set; }
    
    [ForeignKey("CustomerId")] public virtual ApplicationUser Customer { get; set; } = null!;
}