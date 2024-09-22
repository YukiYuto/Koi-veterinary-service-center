using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiVeterinaryServiceCenter.Model.Domain
{
    public class DoctorService
    {
        public Guid ServiceId { get; set; }
        public Guid DoctorId { get; set; }

        [ForeignKey("DoctorId")] public virtual Doctor Doctor { get; set; }
        [ForeignKey("ServiceId")] public virtual Service Service { get; set; }
    }
}
