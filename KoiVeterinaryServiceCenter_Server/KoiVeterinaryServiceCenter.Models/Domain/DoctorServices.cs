using System.ComponentModel.DataAnnotations.Schema;

namespace KoiVeterinaryServiceCenter.Models.Domain;

public class DoctorServices
{
    public Guid ServiceId { get; set; }
    public Guid DoctorId { get; set; }

    [ForeignKey("DoctorId")] public virtual Doctor Doctor { get; set; } = null!;
    [ForeignKey("ServiceId")] public virtual Service Service { get; set; } 
}