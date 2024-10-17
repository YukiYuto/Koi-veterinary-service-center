using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiVeterinaryServiceCenter.Model.Domain
{
    public class DoctorSchedules : BaseEntity<string, string, int>
    {
        [Key] public Guid DoctorSchedulesId { get; set; }
        public Guid DoctorId { get; set; }
        [ForeignKey("DoctorId")] public virtual Doctor Doctor { get; set; }
        public DateTime SchedulesDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}
