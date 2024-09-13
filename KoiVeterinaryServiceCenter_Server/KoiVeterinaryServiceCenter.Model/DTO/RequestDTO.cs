using KoiVeterinaryServiceCenter.Utility.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiVeterinaryServiceCenter.Model.DTO
{
    public class RequestDTO
    {
        public StaticEnum.ApiType ApiType { get; set; } = StaticEnum.ApiType.GET;
        public string Url { get; set; }
        public object Data { get; set; }
        public string AccessToken { get; set; }
        public StaticEnum.ContentType ContentType { get; set; } = StaticEnum.ContentType.Json;
    }
}
