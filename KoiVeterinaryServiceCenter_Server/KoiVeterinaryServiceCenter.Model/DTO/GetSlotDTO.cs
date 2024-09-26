using KoiVeterinaryServiceCenter.Model.Domain;

namespace KoiVeterinaryServiceCenter.Model.DTO;

public class GetSlotDTO : BaseEntity<string, string, int>
{
    public Guid SlotId { get; set; }
    public Guid DoctorId { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public DateTime AppointmentDate { get; set; }
    public bool IsBooked { get; set; }
}