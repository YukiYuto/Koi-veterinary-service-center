using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiVeterinaryServiceCenter.Models.DTO.DoctorRating
{
    public class UpdateDoctorRatingDTO
    {
        public Guid DoctorRatingId { get; set; }
        public Guid DoctorId { get; set; }
        public string DoctorName { get; set; } = string.Empty;

        [Range(1, 5)]
        public int Rating { get; set; }

        public string? Feedback { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
