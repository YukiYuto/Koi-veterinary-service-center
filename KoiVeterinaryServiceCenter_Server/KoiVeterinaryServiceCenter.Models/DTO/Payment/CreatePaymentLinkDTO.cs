using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KoiVeterinaryServiceCenter.Model.Domain;

namespace KoiVeterinaryServiceCenter.Models.DTO.Payment
{
    public class CreatePaymentLinkDTO
    {
        public long AppointmentNumber { get; set; }
        public string CustomerId { get; set; }
        public string CancelUrl { get; set; }
        public string ReturnUrl { get; set; }
    }
}