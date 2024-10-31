using KoiVeterinaryServiceCenter.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiVeterinaryServiceCenter.DataAccess.IRepository
{
   public interface IPetRepository : IRepository<Pet>
    {
        Task<Pet?> GetByIdAsync(Guid petId);
        Task<List<Pet>> GetAllPetsAsync();
        Task<List<Pet>> GetPetbyCustomerId(string customerId);
        void Update(Pet pet);
        void UpdateRange(IEnumerable<Pet> pets);
    }
}
