namespace KoiVeterinaryServiceCenter.Models.DTO.AppointmentPet;

public class CreateAppointmentDepositDTO
{
    public Guid AppointmentId { get; set; }
    public Guid PetId { get; set; }
}