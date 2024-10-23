using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiVeterinaryServiceCenter.Models.DTO.DoctorServices
{
    public class CreateDoctorServicesDTO
    {
        public Guid ServiceId { get; set; }
        public Guid DoctorId { get; set; }
    }
}
