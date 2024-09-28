using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiVeterinaryServiceCenter.Model.Domain
{
    public class DoctorRating : BaseEntity<string, string, int>
    {
        [Key]public Guid DoctorRatingId { get; set; }
        public Guid DoctorId { get; set; }
        [ForeignKey("DoctorId")] public virtual Doctor Doctor { get; set; }
        public int Rating { get; set; }
    }
}
