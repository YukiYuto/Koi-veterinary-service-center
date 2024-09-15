using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiVeterinaryServiceCenter.Model.Domain
{
    public class RefreshTokens : BaseEntity<string, string, int>
    {
        [Key]public Guid RefeshTokensId { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")] public virtual ApplicationUser ApplicationUser { get; set; }
        public string RefreshToken { get; set; }
        public DateTime Expires { get; set; }
    }
}
