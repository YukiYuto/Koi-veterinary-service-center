using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KoiVeterinaryServiceCenter.Models.Domain;

public class Disease
{
    [Key]public Guid DiseaseId { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public string Symptoms { get; set; } = null!;
    
    [NotMapped]public virtual ICollection<PetDisease> PetDiseases { get; set; }
}