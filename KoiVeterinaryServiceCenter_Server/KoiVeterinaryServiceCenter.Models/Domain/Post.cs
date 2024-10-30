using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiVeterinaryServiceCenter.Models.Domain
{
    public class Post : BaseEntity<string, string, int>
    {
        public Guid PostId { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? PostUrl { get; set; }
    }
}
