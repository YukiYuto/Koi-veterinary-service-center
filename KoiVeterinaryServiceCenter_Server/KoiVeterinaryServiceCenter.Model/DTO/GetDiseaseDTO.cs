using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiVeterinaryServiceCenter.Model.DTO
{

    public class GetDiseaseDTO
    {
        public Guid DiseaseId { get; set; }
        public string DiseaseName { get; set; }
        public string DiseaseDescription { get; set; }
        public string DiseaseSymptoms { get; set; }
        public string DiseaseTreatment { get; set; }
    }
}

