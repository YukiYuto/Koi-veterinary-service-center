using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiVeterinaryServiceCenter.Models.DTO.DoctorSchedules
{
    public class CreateDoctorSchedulesDTO
    {
        public Guid DoctorId { get; set; }
        public DateOnly SchedulesDate { get; set; }
    }
}
