using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiVeterinaryServiceCenter.Model.DTO
{
    public class ForgotPasswordDTO
    {
        [Required(ErrorMessage = "Please enter email or phone number.")]
        public string EmailOrPhone { get; set; }
    }
}
