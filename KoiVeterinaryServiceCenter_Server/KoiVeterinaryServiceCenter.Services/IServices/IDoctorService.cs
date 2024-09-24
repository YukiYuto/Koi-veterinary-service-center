using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KoiVeterinaryServiceCenter.Model.Domain;
using KoiVeterinaryServiceCenter.Model.DTO;
using Microsoft.AspNetCore.Identity;

namespace KoiVeterinaryServiceCenter.Services.IServices
{
    public interface IDoctorService
    {
        Task<ResponseDTO> GetDoctorById(Guid id);
        Task<ResponseDTO> UpdateDoctorById(UpdateDoctorDTO updateDoctorDTO);
        Task<ResponseDTO> DeleteDoctorById(String id);
    }
}