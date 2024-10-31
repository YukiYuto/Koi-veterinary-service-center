using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiVeterinaryServiceCenter.Models.DTO.Pool
{
    public class UpdatePoolDTO
    {
        public Guid PoolId { get; set; }
        public string Size { get; set; }
        public string? Decription { get; set; }
    }
}
