using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiVeterinaryServiceCenter.Model.Domain
{
    public class Customer
    {
        [Key] public Guid CustomerId { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")] public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
