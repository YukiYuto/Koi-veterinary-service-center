using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KoiVeterinaryServiceCenter.Model.Domain;

namespace KoiVeterinaryServiceCenter.Model.DTO
{
    public class GetDoctorSchedulesDTO
    {
        public Guid DoctorSchedulesId { get; set; }
        public Guid DoctorId { get; set; }
        public DateTime SchedulesDate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
