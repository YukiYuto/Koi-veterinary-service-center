using KoiVeterinaryServiceCenter.DataAccess.Context;
using KoiVeterinaryServiceCenter.DataAccess.IRepository;
using KoiVeterinaryServiceCenter.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KoiVeterinaryServiceCenter.DataAccess.Repository
{
    public class DiseaseRepository : Repository<Disease>, IDiseaseRepository
    {
        private readonly DbContext _context;

        public DiseaseRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Disease> GetByIdAsync(Guid diseaseId)
        {
            return await _context.Set<Disease>()
                .FirstOrDefaultAsync(d => d.DiseaseId == diseaseId);
        }

        public async Task<List<Disease>> GetByPetIdAsync(Guid petId)
        {
            return await _context.Set<PetDisease>()
                .Where(pd => pd.PetId == petId)
                .Select(pd => pd.Disease)
                .ToListAsync();
        }

        public void Update(Disease disease)
        {
            _context.Set<Disease>().Update(disease);
        }

       

        public void UpdateRange(IEnumerable<Disease> diseases)
        {
            _context.Set<Disease>().UpdateRange(diseases);
        }

      
    }
}
