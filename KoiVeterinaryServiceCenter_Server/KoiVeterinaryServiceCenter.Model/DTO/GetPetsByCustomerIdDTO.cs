using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiVeterinaryServiceCenter.Model.DTO
{
    public class GetPetsByCustomerIdDTO
    {
        public Guid PetId { get; set; }
        public string CustomerId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Species { get; set; }
        public string Breed { get; set; }
        public string Gender { get; set; }

        public string OwnerName { get; set; }
    }
}
