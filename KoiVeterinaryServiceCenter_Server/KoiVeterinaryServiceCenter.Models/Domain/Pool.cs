using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KoiVeterinaryServiceCenter.Models.Domain;

public class Pool
{
    [Key]public Guid PoolId  { get; set; }
    public string? Name { get; set; }
    public string CustomerId { get; set; } = null!;
    public float Size { get; set; }
    
    [ForeignKey("CustomerId")] public virtual ApplicationUser Customer { get; set; } = null!;
}