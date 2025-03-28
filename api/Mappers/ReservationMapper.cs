using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.ReservationDto;
using api.Models;

namespace api.Mappers
{
    public static class ReservationMapper
    {
        public static Reservation toReservation(this AddReservationDto reservationDto, int serviceId, string userId)
        {
            return new Reservation
            {
                AppUserId=userId,
                ServiceId=serviceId,
                Address=reservationDto.Address,
                 Date = TimeZoneInfo.ConvertTimeToUtc(
            DateTime.ParseExact(reservationDto.Date, "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture)
        )
            };
        }
        public static ReservationDto asDto(this Reservation model,string userEmail, string serviceName)
        {
            return new ReservationDto
            {
                AppUserEmail=userEmail,
                ServiceName=serviceName,
                Address=model.Address,
                Date=model.Date.ToString("yyyy-MM-ddTHH:mm:ss")
            };
        }
        public static GetUserReservationsDto userReservationsAsDto(this Reservation model)
        {
            return new GetUserReservationsDto
            {
                Id=model.Id,
                ServiceName=model.Service.Name,
                Address=model.Address,
                Date=model.Date.ToString("yyyy-MM-ddTHH:mm:ss")
            };
        }
        public static GetReservedDateDto getReservedDate(this Reservation model)
        {
            return new GetReservedDateDto
            {
                Date=model.Date.ToString("yyyy-MM-ddTHH:mm:ss")
            };
        }
    }
}