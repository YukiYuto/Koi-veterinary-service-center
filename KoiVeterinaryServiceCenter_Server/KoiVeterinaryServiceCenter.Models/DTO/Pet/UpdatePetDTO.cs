﻿namespace KoiVeterinaryServiceCenter.Models.DTO.Pet
{
    public class UpdatePetDTO
    {
        public Guid PetId { get; set; }
        public string Name { get; set; }
        public string? Species { get; set; }
        public string? Breed { get; set; }
        public string? PetUrl { get; set; }
    }
}
