﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiVeterinaryServiceCenter.Models.DTO.DoctorServices
{
    public class GetDoctorServicesDTO
    {
        public Guid ServiceId { get; set; }
        public Guid DoctorId { get; set; }
        public string? ServiceName { get; set; }
        public string? DoctorFullName { get; set; }
        public string? DoctorUrl { get; set; }
        public string Specialization { get; set; } = null!;
        public string Experience { get; set; } = null!;
        public string Degree { get; set; } = null!;
        public double? Price { get; set; }
        public double? TravelFee { get; set; }
        public DateTime? CreatedTime { get; set; }
    }
}
