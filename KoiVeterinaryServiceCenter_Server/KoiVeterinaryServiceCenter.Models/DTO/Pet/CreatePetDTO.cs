namespace KoiVeterinaryServiceCenter.Models.DTO.Pet
{
    public class CreatePetDTO
    {
        public string Name { get; set; }
        public string? Species { get; set; }
        public string? Breed { get; set; }
        public string CustomerId { get; set; }
        public string? PetUrl { get; set; }
    }
}