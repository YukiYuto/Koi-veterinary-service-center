using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiVeterinaryServiceCenter.Model.Domain
{
    public class AppointmentDetail : BaseEntity<string, string, int>
    {
        [Key]
        public Guid AppointmentDetailId { get; set; }
        public string Description { get; set; } = null!;
        public Guid AppointmentId { get; set; }
        [ForeignKey("AppointmentId")] public Appointment Appointment { get; set; } = null!;

    }
}
