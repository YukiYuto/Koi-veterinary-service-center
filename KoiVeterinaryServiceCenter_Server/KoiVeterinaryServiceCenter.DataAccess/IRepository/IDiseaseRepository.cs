using KoiVeterinaryServiceCenter.Model.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KoiVeterinaryServiceCenter.DataAccess.IRepository
{
    public interface IDiseaseRepository : IRepository<Disease>
    {
        Task<Disease> GetDiseaseById(Guid diseaseId);
        void Update(Disease disease);
        void UpdateRange(IEnumerable<Disease> diseases);
    }
}