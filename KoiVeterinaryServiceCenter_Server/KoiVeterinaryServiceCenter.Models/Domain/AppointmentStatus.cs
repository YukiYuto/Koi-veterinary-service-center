using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KoiVeterinaryServiceCenter.Models.Domain;

public class AppointmentStatus
{
    [Key]public int Id { get; set; }
    public Guid? AppointmentId { get; set; }
    [ForeignKey("AppointmentId")] public Appointment? Appointment { get; set; }
    public int Status { get; set; }

    public string StatusDescription
    {
        get
        {
            switch (Status)
            {
                case 0:
                    return "Pending"; // Chưa đến ngày, appointment chưa tới ngày
                case 1:
                    return "Scheduled"; // Đã đến ngày
                case 2:
                    return "In Progress"; // Đang diễn ra
                case 3:
                    return "Completed"; // Đã xong
                default:
                    return "Pending"; // Default là Pending nếu không có case khớp
            }
        }
    }
}