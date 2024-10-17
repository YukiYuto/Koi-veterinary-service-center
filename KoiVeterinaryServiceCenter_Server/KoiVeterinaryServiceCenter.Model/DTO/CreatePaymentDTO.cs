using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiVeterinaryServiceCenter.Model.DTO
{
    public class CreatePaymentDTO
    {
        public Guid PaymentMethodId { get; set; }
        public string MethodName { get; set; }
        public string status { get; set; } //Link payment method
    }
}
