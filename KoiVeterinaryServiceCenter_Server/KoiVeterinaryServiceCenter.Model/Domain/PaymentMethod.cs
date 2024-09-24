using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiVeterinaryServiceCenter.Model.Domain
{
    public class PaymentMethod
    {
        [Key] public Guid PaymentMethodId { get; set; }
        public string MethodName { get; set; }
        public string Status { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
