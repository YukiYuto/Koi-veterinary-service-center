using System.ComponentModel.DataAnnotations;

namespace KoiVeterinaryServiceCenter.Models.DTO
{
    public class CreateDiseaseDTO
    {
        [Required]
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        
        public string Symptoms { get; set; } 

       
        public string Medication { get; set; }
    }
}
