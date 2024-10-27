using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KoiVeterinaryServiceCenter.Model.Domain;

namespace KoiVeterinaryServiceCenter.Models.DTO.DoctorSchedules
{
    public class GetDoctorSchedulesDTO
    {
        public Guid DoctorSchedulesId { get; set; }
        public Guid DoctorId { get; set; }
        public string? DoctorName { get; set; }
<<<<<<< HEAD
        public DateOnly SchedulesDate { get; set; }
=======
        public string? DoctorUrl { get; set; }
        public DateTime SchedulesDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
>>>>>>> 91783fbb9ac994c7a727f9e0f57d94870be001b9
    }
}
