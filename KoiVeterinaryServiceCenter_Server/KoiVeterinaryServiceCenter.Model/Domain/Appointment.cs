using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiVeterinaryServiceCenter.Model.Domain
{
    public class Appointment
    {
        [Key] public Guid AppointmentId { get; set; }
        public Guid PetId { get; set; }
        public Guid SlotId { get; set; }
        public Guid DoctorRatingId { get; set; }
        public Guid ServiceId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string BookingStatus { get; set; }
        public string AppointmentStatus { get; set; }
        public double TotalAmount { get; set; }
        public string Type { get; set; }
        public DateTime CreateTime { get; set; } = DateTime.Now;
        
        [ForeignKey("DoctorRatingId")] public virtual DoctorRating DoctorRating { get; set; }
    }
}
