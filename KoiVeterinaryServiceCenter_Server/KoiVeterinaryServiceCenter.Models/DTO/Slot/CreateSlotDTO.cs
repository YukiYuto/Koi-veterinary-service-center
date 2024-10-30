namespace KoiVeterinaryServiceCenter.Models.DTO.Slot;

public class CreateSlotDTO
{
    public Guid? DoctorId { get; set; }
    public Guid DoctorSchedulesId { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
}