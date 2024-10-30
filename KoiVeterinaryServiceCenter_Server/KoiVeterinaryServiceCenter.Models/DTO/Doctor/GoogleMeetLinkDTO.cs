using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiVeterinaryServiceCenter.Models.DTO.Doctor
{
    public class GoogleMeetLinkDTO
    {
        public Guid doctorId { get; set; }
        public string GoogleMeetLink { get; set; }
    }
}
