using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly ApplicationDBContext _context;
        public ServiceRepository(ApplicationDBContext context)
        {
            _context=context;
        }

        public async Task<Service> AddServiceAsync(Service serviceModel)
        {
            await _context.Service.AddAsync(serviceModel);
            await _context.SaveChangesAsync();
            return serviceModel;
        }

        public async Task<Service> DeleteAsync(Service serviceModel)
        {
            _context.Service.Remove(serviceModel);
            await _context.SaveChangesAsync();
            return serviceModel;
        }

        public async Task<List<Service>> GetAllAsync()
        {
            var offers = await _context.Service.ToListAsync();
            return offers;
        }

        public async Task<Service> GetByIdAsync(int id)
        {
            return await _context.Service.FirstOrDefaultAsync(s=>s.Id==id);
        }
    }
}