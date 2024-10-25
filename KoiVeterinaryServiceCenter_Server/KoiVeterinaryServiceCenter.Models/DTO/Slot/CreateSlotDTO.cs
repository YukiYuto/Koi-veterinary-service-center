namespace KoiVeterinaryServiceCenter.Model.DTO.Slot;

public class CreateSlotDTO
{
    public Guid? DoctorId { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public DateTime AppointmentDate { get; set; }
    public bool IsBooked { get; set; }
}