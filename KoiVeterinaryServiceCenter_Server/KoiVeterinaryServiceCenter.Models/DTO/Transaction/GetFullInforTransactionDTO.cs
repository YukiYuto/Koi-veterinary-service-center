using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiVeterinaryServiceCenter.Models.DTO.Transaction
{
    public class GetFullInforTransactionDTO
    {
        public string DoctorName { get; set; }
        public string Position { get; set; }
        public string DoctorAvatarUrl { get; set; }
        public string NameService { get; set; }
        public double Price { get; set; }
        public double TravelFee { get; set; }
        public DateOnly ScheduleDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public DateTime TransactionDateTime { get; set; }
    }
}