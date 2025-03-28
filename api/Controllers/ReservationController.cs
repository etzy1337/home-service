using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.ReservationDto;
using api.Extension;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/reservation")]
    [ApiController]
    public class ReservationController :ControllerBase
    {
        private readonly UserManager<AppUser>_userManager;
        private readonly IReservationRepository _reservationRepository;
        private readonly IServiceRepository _serviceRepository;
        public ReservationController(UserManager<AppUser> userManager,IReservationRepository reservationRepository,IServiceRepository serviceRepository)
        {
            _userManager=userManager;
            _reservationRepository=reservationRepository;
            _serviceRepository=serviceRepository;
        }
        [HttpGet]
        public async Task<IActionResult>GetById(int id)
        {
            var reservation = await _reservationRepository.GetByIdAsync(id);

            if (reservation==null)return NotFound();

            var user=await _userManager.FindByIdAsync(reservation.AppUserId);
            if(user==null)return NotFound();
            string userEmail=user.Email;

            var service = await _serviceRepository.GetByIdAsync(reservation.ServiceId);
            if(service==null)return NotFound();
            string serviceName=service.Name;

            var result = reservation.asDto(userEmail,serviceName);

            return Ok(result);
        }



        [Authorize]
        [HttpPost("{serviceId}")]
        public async Task<IActionResult> AddReservation(AddReservationDto reservationDto,[FromRoute] int serviceId)
        {
            if(!ModelState.IsValid) return BadRequest();


            var username = User.GetUsername();
            var user = await _userManager.FindByNameAsync(username);

            if(user==null) return Unauthorized("User not found");



            var offer = await _serviceRepository.GetByIdAsync(serviceId);
            if (offer==null)return NotFound("Offer not exist");

            string userId=user.Id;
            

            var reservationModel = reservationDto.toReservation(serviceId,userId);

            await _reservationRepository.AddReserevationAsync(reservationModel);

            return Ok(reservationModel);
        }

        [Authorize]
        [HttpGet("user")]
        public async Task<IActionResult>GetUserReservation()
        {
            var user = await _userManager.FindByNameAsync(User.GetUsername());
            if(user==null)return Unauthorized();
            var reservations = await _reservationRepository.GetUserReservationAsync(user.Id);

            var result = reservations.Select(r => r.userReservationsAsDto()).ToList();
            return Ok(result);
        }
        [HttpDelete]
        [Authorize]
        public async Task<IActionResult>DeleteReservation(int id)
        {
            var user = await _userManager.FindByNameAsync(User.GetUsername());
            string userId = user.Id;
            var result = await _reservationRepository.DeleteReservationAsync(id,userId);
            if(result==null)return NotFound();
            return NoContent();

        }
        [HttpGet("{serviceId}")]
        public async Task<IActionResult>GetReservedDate([FromRoute]int serviceId)
        {
            var reservation = await _reservationRepository.GetReservedDatesAsync(serviceId);

            var result = reservation.Select(r=>r.getReservedDate()).ToList();

            return Ok(result);
        }
    }
}
