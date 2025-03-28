using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.ReservationDto;
using api.Models;

namespace api.Interfaces
{
    public interface IReservationRepository
    {
        Task<Reservation>AddReserevationAsync(Reservation reservationModel);
        Task<Reservation>GetByIdAsync(int id);
        Task<List<Reservation>>GetUserReservationAsync(string id);
        Task<Reservation>DeleteReservationAsync(int id,string userId);
        Task<List<Reservation>>GetReservedDatesAsync(int serviceId);
    }
}
