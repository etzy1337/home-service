using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOs.ServiceDto;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;


namespace api.Controllers
{
    [Route("api/service")]
    [ApiController]
    public class ServiceController :ControllerBase
    {
        private readonly IServiceRepository _serviceRepository;
        public ServiceController(IServiceRepository serviceRepository)
        {
            _serviceRepository=serviceRepository;   
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var offers = await _serviceRepository.GetAllAsync();
            if(offers==null) return NotFound();
            var result = offers.Select(o=>o.toServiceDto());
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> AddOffer(AddServiceDto serviceDto)
        {
            if(!ModelState.IsValid) return BadRequest();

            var service = serviceDto.toService();

            await _serviceRepository.AddServiceAsync(service);

            return Ok(new {message="service added",service});
        }

        [HttpDelete]
        public async Task<IActionResult>DeleteOffer(int id)
        {
            var service = await _serviceRepository.GetByIdAsync(id);

            if(service==null) return BadRequest();

            await _serviceRepository.DeleteAsync(service);

            return NoContent();
        }
    }
}