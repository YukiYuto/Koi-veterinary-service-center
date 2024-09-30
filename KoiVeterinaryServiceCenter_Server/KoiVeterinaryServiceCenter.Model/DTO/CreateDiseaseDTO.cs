using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiVeterinaryServiceCenter.Model.DTO
{
    public class CreateDiseaseDTO
    {


        public string? DiseaseName { get; set; }
        public string? DiseaseDescription { get; set; }
        public string? DiseaseSymptoms { get; set; }
        public string? DiseaseTreatment { get; set; }

    }
}
