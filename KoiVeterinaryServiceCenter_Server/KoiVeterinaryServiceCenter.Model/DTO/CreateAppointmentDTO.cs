﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiVeterinaryServiceCenter.Model.DTO
{
    public class CreateAppointmentDTO
    {
        public Guid PetId { get; set; }
        public Guid SlotId { get; set; }
        public Guid DoctorRatingId { get; set; }
        public Guid ServiceId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int BookingStatus { get; set; } = 0;
        public double TotalAmount { get; set; }
        public string Type { get; set; }

    }
}