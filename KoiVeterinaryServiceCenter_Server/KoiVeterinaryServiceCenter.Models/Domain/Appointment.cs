using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KoiVeterinaryServiceCenter.Models.Domain;

public class Appointment
{
    [Key] public Guid AppointmentId { get; set; }
    public string CustomerId { get; set; } = null!;
    public Guid SlotId { get; set; }
    [ForeignKey("SlotId")]
    public virtual Slot Slot { get; set; } = null!; // Liên kết đến Slot
    public Guid ServiceId { get; set; }
    [ForeignKey("ServiceId")] // Đảm bảo có mối quan hệ với Service
    public virtual Service Service { get; set; } = null!; // Liên kết đến Service
    public string? Description { get; set; }
    public double TotalAmount { get; set; }
    public DateTime CreateTime { get; set; } = DateTime.Now;
    public int BookingStatus { get; set; }

    public string BookingStatusDescription
    {
        get
        {
            switch (BookingStatus)
            {
                case 0:
                    return "Booked"; // Đã book
                case 1:
                    return "Cancelled"; // Đã hủy
                default:
                    return "Booked"; // Default là Booked
            }
        }
    }
}