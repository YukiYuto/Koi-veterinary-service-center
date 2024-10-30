namespace KoiVeterinaryServiceCenter.Models.DTO.AppointmentPet;

public class GetAppointmentPetDTO
{
    public Guid PetId { get; set; }
    public Guid AppointmentId { get; set; }
    public string PetName { get; set; }
    public string Species { get; set; }
    public string Breed { get; set; }
    public string PetUrl { get; set; }
    public string CustomerId { get; set; }
    public string CustomerName { get; set; }
}