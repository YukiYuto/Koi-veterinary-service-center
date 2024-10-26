using KoiVeterinaryServiceCenter.Model.Domain;
using KoiVeterinaryServiceCenter.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiVeterinaryServiceCenter.DataAccess.IRepository
{
   public interface IDoctorRatingRepository : IRepository<DoctorRating>
    {
        Task<DoctorRating?> GetByIdAsync(Guid doctorRatingId);
        Task<List<DoctorRating>> GetByDoctorIdAsync(Guid doctorId);

        Task<double> GetRatesByDoctorIdAsync(Guid doctorId);

        Task<double> GetRatesAsync();
        void Update(DoctorRating doctorRating);
        void UpdateRange(IEnumerable<DoctorRating> doctorRatings);
    }
}



