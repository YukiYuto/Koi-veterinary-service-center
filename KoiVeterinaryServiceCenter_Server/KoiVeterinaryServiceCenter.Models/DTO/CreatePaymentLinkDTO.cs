using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiVeterinaryServiceCenter.Model.DTO
{
    public class CreatePaymentLinkDTO
    {
        public long AppointmentNumber { get; set; }
        public string CancelUrl { get; set; }
        public string ReturnUrl { get; set; }
    }
}