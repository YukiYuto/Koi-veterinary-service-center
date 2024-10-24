using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiVeterinaryServiceCenter.Model.DTO.Service;

public class CreateServiceDTO
{
    public string ServiceName { get; set; }
    public double Price { get; set; }
    public double TravelFee { get; set; }
}