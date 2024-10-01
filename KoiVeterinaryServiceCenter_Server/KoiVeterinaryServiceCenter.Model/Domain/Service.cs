using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiVeterinaryServiceCenter.Model.Domain
{
    public class Service : BaseEntity<string, string, int>
    {
        [Key] public Guid ServiceId { get; set; }
        public string ServiceName { get; set; }
        public double Price { get; set; }
        public double TreavelFree { get; set; }

        [NotMapped]public virtual ICollection<DoctorService> DoctorServices { get; set; }
    }
}
