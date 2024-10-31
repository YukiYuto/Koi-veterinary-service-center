namespace KoiVeterinaryServiceCenter.Models.DTO.Pet
{
    public class GetPetDTO
    {
        public Guid PetId { get; set; }
        public string Name { get; set; } 
        public string? Species { get; set; }
        public string? Description { get; set; }
        public DateTime? CreatedTime { get; set; }
        public string? PetUrl { get; set; }
    }
}