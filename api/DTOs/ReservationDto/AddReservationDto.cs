using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOs.ReservationDto
{
    public class AddReservationDto
    {
        public string Address { get; set; }
        public string Date { get; set; }
    }
}