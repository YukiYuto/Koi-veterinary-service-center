using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiVeterinaryServiceCenter.Models.DTO.DashBoard
{
    public class GetRevenueOfMonthDTO
    {
        public double Amount { get; set; }
        public DateOnly transactionDateTime { get; set; }
        public string TransactionMethod { get; set; }
    }
}
