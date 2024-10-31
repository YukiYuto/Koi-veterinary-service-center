namespace KoiVeterinaryServiceCenter.Models.DTO.AppointmentDeposit;

public class CreateAppointmentDepositDTO
{
    public Guid AppointmentId { get; set; }
    public double DepositAmount { get; set; }
    
}