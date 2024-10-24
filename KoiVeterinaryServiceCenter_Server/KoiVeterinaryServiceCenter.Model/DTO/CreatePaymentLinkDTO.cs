using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Net.payOS.Types;

namespace KoiVeterinaryServiceCenter.Model.DTO
{
    public class CreatePaymentLinkDTO
    {
        public int ServiceNumber { get; set; }
        public string BuyerName { get; set; }
        public string? BuyerEmail { get; set; }
        public string BuyerPhone { get; set; }
        public string BuyerAddress { get; set; }
        public string CancelUrl { get; set; }
        public string ReturnUrl { get; set; }
    }
}
