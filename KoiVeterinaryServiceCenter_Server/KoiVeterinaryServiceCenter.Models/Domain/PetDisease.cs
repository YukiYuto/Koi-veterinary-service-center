using System.ComponentModel.DataAnnotations.Schema;

namespace KoiVeterinaryServiceCenter.Models.Domain;

public class PetDisease
{
    public Guid PetId { get; set; }
    public Guid DiseaseId { get; set; }
    
    [ForeignKey("PetId")] public virtual Pet Pet { get; set; } = null!;
    [ForeignKey("DiseaseId")] public virtual Disease Disease { get; set; } = null!;
}