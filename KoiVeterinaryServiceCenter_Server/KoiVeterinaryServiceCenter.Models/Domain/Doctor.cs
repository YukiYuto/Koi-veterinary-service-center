using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KoiVeterinaryServiceCenter.Models.Domain;

namespace KoiVeterinaryServiceCenter.Model.Domain
{
    public class Doctor
    {
        [Key] public Guid DoctorId { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")] public virtual ApplicationUser ApplicationUser { get; set; }
        public string Specialization { get; set; }
        public string Experience{ get; set; }
        public string Degree { get; set; }

        [NotMapped]public virtual ICollection<DoctorServices> DoctorServices { get; set; }
    }
}
