using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiVeterinaryServiceCenter.Model.DTO
{
    public class GetPetDiseaseDTO
    {
        public Guid PetId { get; set; }
        public string PetName { get; set; }
        public int PetAge { get; set; }
        public string PetSpecies { get; set; }
        public string PetBreed { get; set; }

        public string PetGender { get; set; }

        public Guid DiseaseId { get; set; }
        public string DiseaseName { get; set; }
        public string Symptoms { get; set; }


        public string Description { get; set; }
        public DateTime Date { get; set; }

    }
}
