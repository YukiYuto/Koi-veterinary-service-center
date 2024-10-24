using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiVeterinaryServiceCenter.Model.Domain
{
    public class PaymentTransactions
    {
        [Key]
        public Guid PaymentTransactionId { get; set; }
        public long OrderCode { get; set; }
        public int Amount { get; set; }
        public string Description { get; set; }
        public string BuyerName { get; set; }
        public string? BuyerEmail { get; set; }
        public string BuyerPhone { get; set; }
        public string BuyerAddress { get; set; }
        public string CancelUrl { get; set; }
        public string ReturnUrl { get; set; }
        public long? ExpiredAt { get; set; }
        public string? Signature { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? Status { get; set; }
        
    }
}
