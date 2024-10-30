namespace KoiVeterinaryServiceCenter.Models.DTO.AppointmentPet;

public class CreateAppointmentPetDTO
{
    public Guid AppointmentId { get; set; }
    public Guid PetId { get; set; }
}