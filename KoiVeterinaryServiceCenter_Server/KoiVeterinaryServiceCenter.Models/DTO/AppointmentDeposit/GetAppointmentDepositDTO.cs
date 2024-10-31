namespace KoiVeterinaryServiceCenter.Models.DTO.AppointmentDeposit;

public class GetAppointmentDepositDTO
{
    public Guid appointmentDepositId { get; set; }
    public Guid appointmentId { get; set; }
    public double depositAmount { get; set; }
    public DateTime depositTime { get; set; }
    public long appointmentDepositNumber { get; set; }
    public int depositStatus { get; set; }
}