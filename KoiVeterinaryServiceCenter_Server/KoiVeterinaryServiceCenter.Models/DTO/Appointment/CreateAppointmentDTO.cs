using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiVeterinaryServiceCenter.Models.DTO.Appointment
{
    public class CreateAppointmentDTO
    {
        public Guid SlotId { get; set; }
        public Guid ServiceId { get; set; }
<<<<<<< HEAD
        public string Description { get; set; } = "";
=======
        public DateTime AppointmentDate { get; set; }
        public int BookingStatus { get; set; } = 0;
        public double TotalAmount { get; set; }
        public string? Description { get; set; }

>>>>>>> 91783fbb9ac994c7a727f9e0f57d94870be001b9
    }
}
