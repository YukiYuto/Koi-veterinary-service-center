namespace KoiVeterinaryServiceCenter.Model.DTO.Slot;

public class UpdateSlotDTO
{
    public Guid SlotId { get; set; }
    public Guid? DoctorId { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public DateTime AppointmentDate { get; set; }
    public int IsBooked { get; set; }
}