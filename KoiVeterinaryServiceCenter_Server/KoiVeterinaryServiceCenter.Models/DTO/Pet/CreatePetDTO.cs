namespace KoiVeterinaryServiceCenter.Models.DTO.Pet
{
    public class CreatePetDTO
    {
        public string Name { get; set; }
        public string? Species { get; set; }
        public string? Description { get; set; }
        public string CustomerId { get; set; }
        public string? PetUrl { get; set; }
    }
}