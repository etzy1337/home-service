using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces
{
    public interface IServiceRepository
    {
        Task<List<Service>>GetAllAsync();
        Task<Service>GetByIdAsync(int id);
        Task<Service>AddServiceAsync(Service serviceModel);
        Task<Service>DeleteAsync(Service serviceModel);
    }
}