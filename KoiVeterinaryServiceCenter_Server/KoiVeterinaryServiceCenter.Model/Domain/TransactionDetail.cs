using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiVeterinaryServiceCenter.Model.Domain
{
    public class TransactionDetail : BaseEntity<string, string, int>
    {
        [Key]
        public Guid TransactionDetailId { get; set; }
        public Guid TransactionId { get; set; }
        [ForeignKey("TransactionId")] public virtual Transaction Transaction { get; set; } = null!;
    }
}
