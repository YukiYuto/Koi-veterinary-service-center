using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiVeterinaryServiceCenter.Models.DTO.Pool
{
    public class GetPoolDTO
    {
        public Guid PoolId { get; set; }
        public string CustomerId { get; set; }
        public string? Name { get; set; }
        public float? Size { get; set; }

        public string? PoolUrl { get; set; }
    }
}
