using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KoiVeterinaryServiceCenter.Model.Domain;
using KoiVeterinaryServiceCenter.Models.Domain;

namespace KoiVeterinaryServiceCenter.Models.DTO.DoctorSchedules
{
    public class GetDoctorSchedulesDTO : BaseEntity<string, string, int>
    {
        public Guid DoctorSchedulesId { get; set; }
        public Guid DoctorId { get; set; }
        public DateOnly SchedulesDate { get; set; }
    }
}
