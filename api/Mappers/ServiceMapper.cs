using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.ServiceDto;
using api.Models;

namespace api.Mappers
{
    public static class ServiceMapper
    {
        public static Service toService(this AddServiceDto dto)
        {
            return new Service
            {
                Name=dto.Name,
            };
        }
        public static GetServiceDto toServiceDto(this Service service)
        {
            return new GetServiceDto
            {
                Id=service.Id,
                Name=service.Name,
            };
        }
    }
}