using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiVeterinaryServiceCenter.Models.DTO.Doctor
{
    public class DoctorInfoDTO
    {
        public string? DoctorId { get; set; }
        public string UserId { get; set; }
        public string? FullName { get; set; }
        public string? Gender { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? BirthDate { set; get; }
        public string? Country { set; get; }
        public string Specialization { get; set; }
        public string Experience { get; set; }
        public string Degree { get; set; }

        public string Position { get; set; }
    }
}
