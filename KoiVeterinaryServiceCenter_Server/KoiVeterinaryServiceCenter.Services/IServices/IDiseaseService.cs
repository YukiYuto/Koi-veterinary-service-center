using KoiVeterinaryServiceCenter.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiVeterinaryServiceCenter.Services.IServices
{
    public interface IDiseaseService
    {
        Task<ResponseDTO> CreateDisease(CreateDiseaseDTO createDiseaseDto);
        Task<ResponseDTO> UpdateDisease(Guid diseaseId, UpdateDiseaseDTO updateDiseaseDto);
        Task<ResponseDTO> GetDisease(Guid diseaseId);
        Task<ResponseDTO> DeleteDisease(Guid diseaseId);

        Task<ResponseDTO> GetAllDisease();
    }
}
