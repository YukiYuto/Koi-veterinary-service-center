using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiVeterinaryServiceCenter.Model.DTO
{
    public class UpdatePetDiseaseDTO
    {
        public Guid PetId { get; set; }

        public Guid DiseaseId { get; set; }

        public string? Description { get; set; }
        public DateTime Date { get; set; }
    }
}
