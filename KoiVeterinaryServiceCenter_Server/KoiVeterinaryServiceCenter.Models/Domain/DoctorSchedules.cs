using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KoiVeterinaryServiceCenter.Model.Domain;

namespace KoiVeterinaryServiceCenter.Models.Domain;

public class DoctorSchedules : BaseEntity<string, string, int>
{
    [Key] public Guid DoctorSchedulesId { get; set; }
    public Guid DoctorId { get; set; }
    [ForeignKey("DoctorId")] public virtual Doctor Doctor { get; set; }
    public DateOnly SchedulesDate { get; set; }

    public virtual ICollection<Slot> Slots { get; set; } = new List<Slot>();
}