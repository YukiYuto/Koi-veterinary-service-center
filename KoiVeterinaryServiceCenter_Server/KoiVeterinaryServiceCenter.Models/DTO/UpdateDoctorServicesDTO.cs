using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiVeterinaryServiceCenter.Model.DTO
{
    public class UpdateDoctorServicesDTO
    {
        public Guid DoctorServiceId { get; set; }
        public Guid DoctorId { get; set; }
    }
}
