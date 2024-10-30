using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KoiVeterinaryServiceCenter.Models.Domain;

namespace KoiVeterinaryServiceCenter.DataAccess.IRepository
{
    public interface IPetServiceRepository : IRepository<PetService>
    {
        Task<PetService> GetById(Guid petId);
        void Update(PetService petService);
    }
}
