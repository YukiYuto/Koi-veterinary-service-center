using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KoiVeterinaryServiceCenter.Models.Domain;

public class AppointmentPet
{
    public Guid AppointmentId { get; set; }
    [ForeignKey("AppointmentId")] public virtual Appointment Appointment { get; set; } = null!;
    public Guid PetId { get; set; }
    [ForeignKey("PetId")] public virtual Pet Pet { get; set; } = null!;
}