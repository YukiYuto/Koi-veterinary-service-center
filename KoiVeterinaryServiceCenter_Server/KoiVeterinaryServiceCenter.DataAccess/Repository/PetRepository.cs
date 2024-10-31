using KoiVeterinaryServiceCenter.DataAccess.Context;
using KoiVeterinaryServiceCenter.DataAccess.IRepository;
using KoiVeterinaryServiceCenter.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace KoiVeterinaryServiceCenter.DataAccess.Repository
{
    public class PetRepository : Repository<Pet>, IPetRepository
    {
        private readonly ApplicationDbContext _context;

        public PetRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Pet?> GetByIdAsync(Guid petId)
        {
            return await _context.Pets
                .Include(p => p.Customer)
                .FirstOrDefaultAsync(p => p.PetId == petId);
        }

        public async Task<List<Pet>> GetAllPetsAsync()
        {
            return await _context.Pets
                .Include(p => p.Customer)
                .ToListAsync();
        }

        public async Task<List<Pet>> GetPetbyCustomerId(string customerId)
        {
            return await _context.Pets
                .Where(p => p.CustomerId == customerId)
                .Include(p => p.Customer)
                .ToListAsync();
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
