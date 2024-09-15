using KoiVeterinaryServiceCenter.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiVeterinaryServiceCenter.Services.IServices
{
    public interface IAuthService
    {
        Task<ResponseDTO> SignUpDoctor(RegisterDoctorDTO registerDoctorDTO);
        Task<ResponseDTO> SignUpCustomer(RegisterCustomerDTO registerCustomerDTO);
        Task<ResponseDTO> SignIn(SignDTO signDto);

    }
}
