using KoiVeterinaryServiceCenter.Model.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KoiVeterinaryServiceCenter.DataAccess.IRepository
{
    public interface IPetDiseaseRepository : IRepository<PetDisease>
    {
        void Update(PetDisease petDisease);
        void UpdateRange(IEnumerable<PetDisease> petDiseases);
        Task<PetDisease> GetPetDiseaseById(Guid petDiseaseId);
    }
}