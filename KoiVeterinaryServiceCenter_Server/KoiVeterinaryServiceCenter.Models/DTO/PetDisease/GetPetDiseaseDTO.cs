namespace KoiVeterinaryServiceCenter.Models.DTO
{
    public class GetPetDiseaseDTO
    {
        public Guid PetId { get; set; }
        public string PetName { get; set; } = null!;
        public Guid DiseaseId { get; set; }
        public string DiseaseName { get; set; } = null!;

        public DateTime? CreatedTime { get; set; }

    }
}
