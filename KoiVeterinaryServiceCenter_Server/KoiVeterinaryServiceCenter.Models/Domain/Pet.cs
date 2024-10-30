using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KoiVeterinaryServiceCenter.Models.Domain;

public class Pet : BaseEntity<string, string, int>

{
    [Key]public Guid PetId { get; set; }
    public string Name { get; set; } = null!;
    public string? Species { get; set; }
    public string? Breed { get; set; }
    public string CustomerId { get; set; } = null!;
    public string? PetUrl { get; set; }
    [ForeignKey("CustomerId")] public virtual ApplicationUser Customer { get; set; } = null!;
    
    [NotMapped]public virtual ICollection<PetDisease> PetDiseases { get; set; }
    [NotMapped]public virtual ICollection<PetService> PetServices { get; set; }
    [NotMapped]public virtual ICollection<AppointmentPet> AppointmentPets { get; set; }


}