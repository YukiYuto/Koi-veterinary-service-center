using KoiVeterinaryServiceCenter.DataAccess.Context;
using KoiVeterinaryServiceCenter.DataAccess.IRepository;
using KoiVeterinaryServiceCenter.Model.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KoiVeterinaryServiceCenter.DataAccess.Repository
{
    public class PetDiseaseRepository : Repository<PetDisease>, IPetDiseaseRepository
    {
        private readonly ApplicationDbContext _context;

        public PetDiseaseRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<PetDisease> GetPetDiseaseById(Guid petDiseaseId)
        {
            return await _context.PetsDiseases
                .Include(pd => pd.Pet)       // Include related Pet entity
                .Include(pd => pd.Disease)   // Include related Disease entity
                .FirstOrDefaultAsync(x => x.PetDiseaseId == petDiseaseId);
        }

        public void Update(PetDisease petDisease)
        {
            _context.PetsDiseases.Update(petDisease);
        }

        public void UpdateRange(IEnumerable<PetDisease> petDiseases)
        {
            _context.PetsDiseases.UpdateRange(petDiseases);
        }
    }
}
