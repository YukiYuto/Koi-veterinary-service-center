using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiVeterinaryServiceCenter.Models.DTO.PetService
{
    public class CreatePetServiceDTO
    {
        public Guid PetId { get; set; }
        public Guid ServiceId { get; set; }
    }
}
