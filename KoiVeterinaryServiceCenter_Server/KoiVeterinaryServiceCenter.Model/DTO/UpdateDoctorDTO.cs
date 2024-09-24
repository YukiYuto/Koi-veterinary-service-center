using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KoiVeterinaryServiceCenter.Model.DTO
{
    public class UpdateDoctorDTO
    {
        [Required] public Guid DoctorId { get; set; }
        [Required] public string? FullName { get; set; }
        [Required] public string? Gender { get; set; }
        [Required] public string? Email { get; set; }
        [Required] public string? PhoneNumber { get; set; }
        [Required] public DateTime? BirthDate { get; set; }
        public string? AvatarUrl { get; set; }
        [Required] public string? Country { get; set; }
        [Required] public string? Address { get; set; }
        [Required] public string Specialization { get; set; }
        [Required] public string Experience { get; set; }
        [Required] public string Degree { get; set; }
    }
}