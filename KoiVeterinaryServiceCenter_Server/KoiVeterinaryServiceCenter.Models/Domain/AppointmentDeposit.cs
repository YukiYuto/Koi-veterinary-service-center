using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KoiVeterinaryServiceCenter.Models.Domain;

public class AppointmentDeposit
{
    [Key] public Guid DepositId { get; set; }
    
    public Guid AppointmentId { get; set; }
    [ForeignKey("AppointmentId")] public virtual Appointment Appointment { get; set; } = null!;

    public double DepositAmount { get; set; } // Số tiền đặt cọc, thường là 30% của tổng số tiền
    public DateTime DepositTime { get; set; } // Ngày đặt cọc
    
    public long AppointmentDepositNumber { get; set; } // Số đơn đặt cọc
    public int DepositStatus { get; set; } // Trạng thái đặt cọc: 0 = Chưa thanh toán, 1 = Đã thanh toán

   [NotMapped]
    public string DepositStatusDescription
    {
        get { return DepositStatus == 1 ? "Paid" : "Pending"; }
    }
}