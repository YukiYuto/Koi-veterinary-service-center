using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KoiVeterinaryServiceCenter.DataAccess.Context;
using KoiVeterinaryServiceCenter.DataAccess.IRepository;
using KoiVeterinaryServiceCenter.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace KoiVeterinaryServiceCenter.DataAccess.Repository
{
    public class PetServiceRepository : Repository<PetService>, IPetServiceRepository
    {
        private readonly ApplicationDbContext _context;
        public PetServiceRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<PetService> GetById(Guid petId)
        {
            return await _context.PetServices.Include(ps => ps.Pet).Include(ps => ps.Service)
                .FirstOrDefaultAsync(ps => ps.PetId == petId);
        }

        public void Update(PetService petService)
        {
            _context.PetServices.Update(petService);
        }
    }
}
