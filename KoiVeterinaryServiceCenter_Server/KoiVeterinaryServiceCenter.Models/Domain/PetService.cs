using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KoiVeterinaryServiceCenter.Models.Domain;

public class PetService
{
    public Guid PetId { get; set; }
    public Guid ServiceId { get; set; }
    
    [ForeignKey("PetId")] public virtual Pet Pet { get; set; } = null!;
    [ForeignKey("ServiceId")] public virtual Service Service { get; set; } = null!;
}