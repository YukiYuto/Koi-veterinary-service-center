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
    public class DiseaseRepository : Repository<Disease>, IDiseaseRepository
    {
        private readonly ApplicationDbContext _context;

        public DiseaseRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Disease> GetDiseaseById(Guid diseaseId)
        {
            return await _context.Diseases.FirstOrDefaultAsync(x => x.DiseaseId == diseaseId);
        }

        public void Update(Disease disease)
        {
            _context.Diseases.Update(disease);
        }

        public void UpdateRange(IEnumerable<Disease> diseases)
        {
            _context.Diseases.UpdateRange(diseases);
        }
    }
}