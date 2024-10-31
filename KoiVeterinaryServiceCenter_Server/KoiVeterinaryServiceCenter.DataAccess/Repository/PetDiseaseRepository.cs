using KoiVeterinaryServiceCenter.DataAccess.Context;
using KoiVeterinaryServiceCenter.DataAccess.Repository;
using KoiVeterinaryServiceCenter.Models.Domain;
using KoiVeterinaryServiceCenter.Services.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiVeterinaryServiceCenter.Services.Repositories
{
    public class PetDiseaseRepository : Repository<PetDisease> , IPetDiseaseRepository
    {
        private readonly ApplicationDbContext _context;

        public PetDiseaseRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<PetDisease> GetById(Guid petId, Guid diseaseId)
        {
            return await _context.PetDiseases.FirstOrDefaultAsync(pd => pd.PetId == petId && pd.DiseaseId == diseaseId);

        }

        public void Update(PetDisease petDisease)
        {
            _context.PetDiseases.Update(petDisease);
        }

        public void UpdateRange(IEnumerable<PetDisease> petDiseases)
        {
            _context.PetDiseases.UpdateRange(petDiseases);
        }

        public async Task<List<PetDisease>> GetByPetId(Guid petId)
        {
            return await _context.PetDiseases.Include(x => x.Disease).Include(x => x.Pet)
                .Where(pd => pd.PetId == petId)
                .ToListAsync();
        }

        public async Task<List<PetDisease>> GetByDiseaseId(Guid diseaseId)
        {
            return await _context.PetDiseases.Include(x => x.Disease).Include(x => x.Pet)
                .Where(pd => pd.DiseaseId == diseaseId)
                .ToListAsync();
        }
    }
}