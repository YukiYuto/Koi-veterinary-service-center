using KoiVeterinaryServiceCenter.Model.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiVeterinaryServiceCenter.DataAccess.IRepository
{
    public interface IAppointmentRepository : IRepository<Appointment>
    {
        void Update(Appointment appointment);
        void UpdateRange(IEnumerable<Appointment> appointments);
        Task<Appointment> GetAppointmentById(Guid appointmentId);
    }
}
