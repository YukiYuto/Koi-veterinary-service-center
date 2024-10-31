using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiVeterinaryServiceCenter.Models.DTO.Pool
{
    public class GetPoolFullInfo
    {
        public Guid PoolId { get; set; }
        public string CustomerId { get; set; }
        public string? PoolName { get; set; }
        public string? Size { get; set; }
        public string CustomerName { get; set; }
        public string? Gender { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }

        public string? PoolUrl { get; set; }
    }
}
