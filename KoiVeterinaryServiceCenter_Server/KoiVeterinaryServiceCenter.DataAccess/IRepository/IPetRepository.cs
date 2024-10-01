using KoiVeterinaryServiceCenter.Model.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace KoiVeterinaryServiceCenter.DataAccess.IRepository
{
    public interface IPetRepository : IRepository<Pet>
    {
        void Update(Pet pet);
        void UpdateRange(IEnumerable<Pet> pets);
        Task<Pet> GetPetById(Guid petId);

    }
}

