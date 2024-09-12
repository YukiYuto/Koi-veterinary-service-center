using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiVeterinaryServiceCenter.Model.Domain
{
    public class PetDisease
    {
        [Key]public Guid PetDiseaseId { get; set; }
        public Guid PetId { get; set; }
        [ForeignKey("PetId")] public virtual Pet Pet { get; set; }
        public Guid DiseaseId { get; set; }
        [ForeignKey("DiseaseId")] public virtual Disease Disease{ get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}
