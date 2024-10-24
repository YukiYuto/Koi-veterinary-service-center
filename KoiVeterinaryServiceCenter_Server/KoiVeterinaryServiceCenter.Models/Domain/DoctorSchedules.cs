using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KoiVeterinaryServiceCenter.Models.Domain;

namespace KoiVeterinaryServiceCenter.Model.Domain
{
    public class DoctorSchedules : BaseEntity<string, string, int>
    {
        [Key] public Guid DoctorSchedulesId { get; set; }
        public Guid DoctorId { get; set; }
        [ForeignKey("DoctorId")] public virtual Doctor Doctor { get; set; }
        public DateTime SchedulesDate { get; set; }

        public virtual ICollection<Slot> Slots { get; set; } = new List<Slot>();
    }
}
