using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiVeterinaryServiceCenter.Model.Domain
{
    public class OrderItems
    {
        public long Id { get; set; }
        public Guid? TransactionId { get; set; }
        public string ProductName { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
    }
}
