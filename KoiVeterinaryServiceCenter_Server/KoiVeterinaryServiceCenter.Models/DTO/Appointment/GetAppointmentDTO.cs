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
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public Guid ServiceId { get; set; }
        public string ServiceName { get; set; } = null!;
        public Guid PetId { get; set; }
        public string PetName { get; set; } = null!;
        public int BookingStatus { get; set; }
        public string BookingStatusDescription { get; set; } = null!;
        public double TotalAmount { get; set; }
        public DateOnly AppointmentDate { get; set; }
        public string? Description { get; set; }
        public string CustomerId { get; set; } = null!;
        public string CustomerName { get; set; } = null!;
        public long AppointmentNumber { get; set; }
        public string DoctorName { get; set; } = null!;
        public Guid DoctorId { get; set; }
    }
}
