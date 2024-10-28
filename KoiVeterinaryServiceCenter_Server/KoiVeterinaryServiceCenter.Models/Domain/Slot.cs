using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KoiVeterinaryServiceCenter.Models.Domain;

public class Slot : BaseEntity<string, string, int>
{
    [Key] public Guid SlotId { get; set; }
    public Guid DoctorSchedulesId { get; set; }
    [ForeignKey("DoctorSchedulesId")] public virtual DoctorSchedules DoctorSchedules { get; set; }
    public DateOnly SchedulesDate { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public int IsBooked { get; set; }

    public string BookingStatusDescription
    {
        get
        {
            switch (IsBooked)
            {
                case 0:
                    return "Free";
                case 1:
                    return "Pending";
                case 2:
                    return "Booked";
                default:
                    return "Free";
            }
        }
    }

}