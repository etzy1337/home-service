using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOs.ServiceDto
{
    public class GetServiceDto
    {
        public int Id { get; set; }
        public string Name { get; set; }=string.Empty;
    }
}