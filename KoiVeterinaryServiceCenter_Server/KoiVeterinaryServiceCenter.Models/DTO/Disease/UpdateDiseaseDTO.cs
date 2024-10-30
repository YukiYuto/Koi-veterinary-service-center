using System.ComponentModel.DataAnnotations;

namespace KoiVeterinaryServiceCenter.Models.DTO
{
    public class UpdateDiseaseDTO
    {
        [Required]
        public Guid DiseaseId { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public string? Symptoms { get; set; }

        public string? Medication { get; set; }
    }
}
