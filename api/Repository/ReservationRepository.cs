using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOs.ReservationDto;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly ApplicationDBContext _context;
        public ReservationRepository(ApplicationDBContext context)
        {
            _context=context;
        }
        public async Task<Reservation> AddReserevationAsync(Reservation reservationModel)
        {
            await _context.Reservation.AddAsync(reservationModel);
            await _context.SaveChangesAsync();
            return reservationModel;
        }

        public async Task<Reservation> DeleteReservationAsync(int id,int userId)
        {
            var reservation = await _context.Reservation.FirstOrDefaultAsync(r=>r.Id==id);
            if(reservation==null)return null;
            if(userId!=reservation.userId) return null;
            _context.Reservation.Remove(reservation);
            await _context.SaveChangesAsync();
            return reservation;

        }

        public async Task<Reservation> GetByIdAsync(int id)
        {
            return await _context.Reservation.Include(r=>r.Service).FirstOrDefaultAsync(r=>r.Id==id);
        }

        public async Task<List<Reservation>> GetReservedDatesAsync(int serviceId)
        {
            return await _context.Reservation.Where(r=>r.ServiceId==serviceId).ToListAsync();

        }

        public async Task<List<Reservation>> GetUserReservationAsync(string id)
        {
            return await _context.Reservation.Include(r=>r.Service).Where(r=>r.AppUserId==id).ToListAsync();
        }
    }
}