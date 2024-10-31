namespace KoiVeterinaryServiceCenter.Models.DTO
{
    public class UpdatePetDiseaseDTO
    {
        public Guid PetId { get; set; }
        public Guid DiseaseId { get; set; }
    }
}