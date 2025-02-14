using Ability_Drive_API.Data;

namespace Ability_Drive_API.DTOs
{
    public class SeatBookingDTO
    {
        public int Id { get; set; }
        public int BusScheduleId { get; set; }
        public int UserId { get; set; }
        public bool IsDisabledPassenger { get; set; }
        public DateTime BookingTime { get; set; }
    }
    public class SeatBookingDTOForOther
    {
        public int Id { get; set; }
        public string BusName { get; set; }
        public bool IsDisabledPassenger { get; set; }
        public DateTime BookingTime { get; set; }
    }

}
