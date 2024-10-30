using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiVeterinaryServiceCenter.Models.DTO.Appointment
{
    public class GetAppointmentDTO
    {
        public Guid AppointmentId { get; set; }
        public Guid SlotId { get; set; }
        public Guid ServiceId { get; set; }
        public string BookingStatus { get; set; }
        public double TotalAmount { get; set; }
        public DateOnly CreateTime { get; set; }
        public string Description { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int AppointmentNumber { get; set; }
    }
}
