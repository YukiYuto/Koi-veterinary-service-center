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
        [ForeignKey("DoctorRatingId")] public virtual DoctorRating DoctorRating { get; set; }
        public Guid ServiceId { get; set; }
        public DateTime AppointmentDate { get; set; }


        public double TotalAmount { get; set; }
        public string Type { get; set; }
        public DateTime CreateTime { get; set; } = DateTime.Now;

        public int BookingStatus { get; set; }

        public string BookingStatusDescription
        {
            get
            {
                switch (BookingStatus)
                {
                    case 0:
                        return "Booked"; // Đã book
                    case 1:
                        return "Cancelled"; // Đã hủy
                    default:
                        return "Booked"; // Default là Booked
                }
            }
        }


    }
}
