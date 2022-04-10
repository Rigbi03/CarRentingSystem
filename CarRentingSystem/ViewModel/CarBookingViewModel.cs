using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarRentingSystem.ViewModel
{
    public class CarBookingViewModel
    {
        public int BookingId { get; set; }
        public string CustomerName { get; set; }
        public int NumberOfMembers { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerPhoneNumber { get; set; }
        public DateTime BookingFrom { get; set; }
        public decimal CarPrice { get; set; }
        public DateTime BookingTo { get; set; }
        public string CarNumber { get; set; }
        public decimal TotalAmount { get; set; }
        public int NumberOfDays { get; set; }
    }
}