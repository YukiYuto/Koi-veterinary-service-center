using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiVeterinaryServiceCenter.Model.DTO
{
    public class CreatePaymentLinkDTO
    {
<<<<<<< HEAD
        public long OrderCode { get; set; }
        public string BuyerName { get; set; }
        public string? BuyerEmail { get; set; }
        public string BuyerPhone { get; set; }
        public string BuyerAddress { get; set; }
=======
        public long AppointmentNumber { get; set; }
>>>>>>> 91783fbb9ac994c7a727f9e0f57d94870be001b9
        public string CancelUrl { get; set; }
        public string ReturnUrl { get; set; }
    }
}