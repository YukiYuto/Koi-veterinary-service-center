using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using KoiVeterinaryServiceCenter.Model.Domain;

namespace KoiVeterinaryServiceCenter.Model.DTO
{
    public class GetServiceDTO : BaseEntity<string, string, int>
    {
        public Guid ServiceId { get; set; }
        public string ServiceName { get; set; }
        public double Price { get; set; }
        public double TreavelFree { get; set; }
    }
}
