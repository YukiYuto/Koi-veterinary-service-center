using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KoiVeterinaryServiceCenter.Models.Domain;

public class DoctorSchedules : BaseEntity<string, string, int>
{
    [Key] public Guid DoctorSchedulesId { get; set; }
    public Guid DoctorId { get; set; }
    [ForeignKey("DoctorId")] public virtual Doctor Doctor { get; set; } = null!;
    public DateTime SchedulesDate { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
}