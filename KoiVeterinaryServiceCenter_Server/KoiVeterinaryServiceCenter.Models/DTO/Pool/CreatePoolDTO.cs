using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiVeterinaryServiceCenter.Models.DTO.Pool
{
    public class CreatePoolDTO
    {
        public string? Name { get; set; }
        public string CustomerId { get; set; } = null!;

        public string Size { get; set; }
        public string? PoolUrl { get; set; }


    }
}
