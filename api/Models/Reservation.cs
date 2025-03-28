using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public int ServiceId { get; set; }
        public Service Service { get; set; }
        public string Address { get; set; }
        public DateTime Date { get; set; }
    }
}