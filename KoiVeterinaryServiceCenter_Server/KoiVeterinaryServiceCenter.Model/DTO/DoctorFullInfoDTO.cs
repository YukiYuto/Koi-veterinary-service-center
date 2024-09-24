using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KoiVeterinaryServiceCenter.Model.DTO
{
    public class DoctorFullInfoDTO
    {
        public Guid? DoctorId { get; set; }
        public string UserId { get; set; }
        public string? FullName { get; set; }
        public string? Gender { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? AvatarUrl { get; set; }
        public string? Country { get; set; }
        public string? Address { get; set; }
        public string Specialization { get; set; }
        public string Experience { get; set; }
        public string Degree { get; set; }
    }
}