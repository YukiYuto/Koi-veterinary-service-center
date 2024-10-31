namespace KoiVeterinaryServiceCenter.Models.DTO
{
    public class CreatePetDiseaseDTO
    {
        public Guid PetId { get; set; }
        public Guid DiseaseId { get; set; }
    }
}