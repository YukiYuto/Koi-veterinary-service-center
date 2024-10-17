using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KoiVeterinaryServiceCenter.Models.Domain;

public class AppointmentDetail : BaseEntity<string, string, int>
{
    [Key]
    public Guid AppointmentDetailId { get; set; }
    public string Description { get; set; } = null!;
    public Guid AppointmentId { get; set; }
    [ForeignKey("AppointmentId")] public Appointment Appointment { get; set; } = null!;

}