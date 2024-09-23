using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KoiVeterinaryServiceCenter.Model.DTO
{
    public class UpdateDoctorDTO
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "FullName is required")]
        public string FullName { get; set; }


        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Country is required")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Birth date is required.")]
        [DataType(DataType.Date, ErrorMessage = "Invalid date format.")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [Phone(ErrorMessage = "Provided phone number is not valid.")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Provided phone number not valid.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Specialization is required")]
        public string Specialization { get; set; }

        [Required(ErrorMessage = "Experience is required")]
        public string Experience { get; set; }

        [Required(ErrorMessage = "Degree is required")]
        public string Degree { get; set; }
    }
}