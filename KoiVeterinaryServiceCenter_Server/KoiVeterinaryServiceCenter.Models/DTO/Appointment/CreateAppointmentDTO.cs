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
        public string Description { get; set; } = "";
    }
}
