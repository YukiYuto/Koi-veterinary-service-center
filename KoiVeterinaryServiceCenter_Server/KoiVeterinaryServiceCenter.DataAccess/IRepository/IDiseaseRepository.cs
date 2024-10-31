using KoiVeterinaryServiceCenter.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiVeterinaryServiceCenter.DataAccess.IRepository
{
    public interface IDiseaseRepository : IRepository<Disease>
    {
        Task<Disease> GetByIdAsync(Guid diseaseId);
      

       
        void Update(Disease disease);
        void UpdateRange(IEnumerable<Disease> diseases);
    }
}
