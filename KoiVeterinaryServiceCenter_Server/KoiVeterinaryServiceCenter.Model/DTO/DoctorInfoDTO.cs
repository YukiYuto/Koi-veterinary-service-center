using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KoiVeterinaryServiceCenter.Model.DTO
{
    public class DoctorInfoDTO
    {
        public Guid? DoctorId { get; set; }
        public string UserId { get; set; }
        public string Specialization { get; set; }
        public string Experience { get; set; }
        public string Degree { get; set; }
    }
}