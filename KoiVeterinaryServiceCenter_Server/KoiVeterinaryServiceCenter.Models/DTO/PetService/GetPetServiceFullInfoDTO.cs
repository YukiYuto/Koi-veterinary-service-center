using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiVeterinaryServiceCenter.Models.DTO.PetService
{
    public class GetPetServiceFullInfoDTO
    {
        public Guid PetId { get; set; }
        public Guid ServiceId { get; set; }
        public string PetName { get; set; }
        public string Species { get; set; }
        public string Breed { get; set; }
        public string ServiceName { get; set; }
        public string Price { get; set; }
    }
}
