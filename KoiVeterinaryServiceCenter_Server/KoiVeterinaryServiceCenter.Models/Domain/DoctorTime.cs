namespace KoiVeterinaryServiceCenter.Models.Domain;

public class DoctorTime
{
    public Guid DoctorTimeId { get; set; }
    public Guid DoctorId { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
}