using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KoiVeterinaryServiceCenter.DataAccess.IRepository;
using KoiVeterinaryServiceCenter.Models.Domain;

namespace KoiVeterinaryServiceCenter.Services.IRepositories
{
    public interface IPetDiseaseRepository : IRepository<PetDisease>
    {
        Task<PetDisease> GetById(Guid petId, Guid diseaseId);
        void Update(PetDisease petDisease);
        void UpdateRange(IEnumerable<PetDisease> petDiseases);

        Task<List<PetDisease>> GetByPetId(Guid petId);
        Task<List<PetDisease>> GetByDiseaseId(Guid diseaseId);
    }
}