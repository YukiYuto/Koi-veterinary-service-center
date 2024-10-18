using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KoiVeterinaryServiceCenter.Model.Domain;

namespace KoiVeterinaryServiceCenter.Models.Domain;

public class Slot : BaseEntity<string, string, int>
{
    [Key] public Guid SlotId { get; set; }
    public Guid DoctorId { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public DateTime AppointmentDate { get; set; }
    public bool IsBooked { get; set; }

    [ForeignKey("DoctorId")] public virtual Doctor? Doctor { get; set; }
}