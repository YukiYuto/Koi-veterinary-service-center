using KoiVeterinaryServiceCenter.DataAccess.Context;
using KoiVeterinaryServiceCenter.DataAccess.IRepository;
using KoiVeterinaryServiceCenter.Model.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiVeterinaryServiceCenter.DataAccess.Repository
{
    public class PetRepository : Repository<Pet>, IPetRepository
    {
        private readonly ApplicationDbContext _context;

        public PetRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Pet> GetPetById(Guid PetId)
        {
            return await _context.Pets.FirstOrDefaultAsync(x => x.PetId == PetId);
        }

        public void Update(Pet pet)
        {
            _context.Pets.Update(pet);
        }

        public void UpdateRange(IEnumerable<Pet> pets)
        {
            _context.Pets.UpdateRange(pets);
        }
    }
}
