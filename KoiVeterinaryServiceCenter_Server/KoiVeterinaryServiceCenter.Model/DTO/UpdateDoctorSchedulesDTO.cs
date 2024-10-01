using System.ComponentModel.DataAnnotations;

namespace KoiVeterinaryServiceCenter.Model.DTO
{
    public class UpdateDoctorSchedulesDTO
    {
        public Guid DoctorSchedulesId { get; set; }
        public Guid? DoctorId { get; set; }
        public DateTime SchedulesDate { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }
    }
}


