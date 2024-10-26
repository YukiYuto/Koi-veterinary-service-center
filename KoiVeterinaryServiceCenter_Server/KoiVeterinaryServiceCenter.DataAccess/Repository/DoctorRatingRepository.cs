using KoiVeterinaryServiceCenter.DataAccess.Context;
using KoiVeterinaryServiceCenter.DataAccess.IRepository;
using KoiVeterinaryServiceCenter.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiVeterinaryServiceCenter.DataAccess.Repository
{
    public class DoctorRatingRepository : Repository<DoctorRating>, IDoctorRatingRepository
    {
        private readonly ApplicationDbContext _context;

        public DoctorRatingRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<DoctorRating?> GetByIdAsync(Guid doctorRatingId)
        {
            return await _context.DoctorRatings
                .Include(dr => dr.Doctor)
                .FirstOrDefaultAsync(dr => dr.DoctorRatingId == doctorRatingId);
        }

        public async Task<List<DoctorRating>> GetByDoctorIdAsync(Guid doctorId)
        {
            return await _context.DoctorRatings
                .Include(dr => dr.Doctor)
                .Where(dr => dr.DoctorId == doctorId)
                .ToListAsync();
        }
        public async Task<double> GetRatesByDoctorIdAsync(Guid doctorId)
        {
            return await _context.DoctorRatings
                .Where(dr => dr.DoctorId == doctorId)
                .AverageAsync(dr => (double?)dr.Rating) ?? 0; // Handle cases with no ratings
        }

        public async Task<double> GetRatesAsync()
        {
            return await _context.DoctorRatings
                .AverageAsync(dr => (double?)dr.Rating) ?? 0; // Handle cases with no ratings
        }

        public void Update(DoctorRating doctorRating)
        {
            _context.DoctorRatings.Update(doctorRating);
        }

        public void UpdateRange(IEnumerable<DoctorRating> doctorRatings)
        {
            _context.DoctorRatings.UpdateRange(doctorRatings);
        }
    }
}
