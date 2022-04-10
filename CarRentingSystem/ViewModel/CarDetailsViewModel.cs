using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarRentingSystem.ViewModel
{
    public class CarDetailsViewModel
    {
        public int CarId { get; set; }
        public string CarNumber { get; set; }
        public string CarImage { get; set; }
        public decimal CarPrice { get; set; }
        public string BookingStatus { get; set; }
        public string CarBrand { get; set; }
        public string CarModel { get; set; }
        public int CarYear { get; set; }
        public int CarCapacity { get; set; }
        public string CarDescription { get; set; }
    }
}