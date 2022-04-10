using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CarRentingSystem.ViewModel
{
    public class CarViewModel
    {
        public int CarId { get; set; }

        [Display(Name = "Car No.")]
        [Required(ErrorMessage = "Car Number is required." )]
        public string CarNumber { get; set; }

        [Display(Name = "Car Image")]
        [Required(ErrorMessage = "Car Image is required.")]
        public string CarImage { get; set; }

        [Display(Name = "Car Price in $ per day")]
        [Required(ErrorMessage = "Car Price is required.")]
        [Range(250,5500, ErrorMessage ="Car Price should be between {1} and {2}")]
        public decimal CarPrice { get; set; }

        [Display(Name = "Booking Status")]
        [Required(ErrorMessage = "Booking Status is required.")]

        public int BookingStatusId { get; set; }

        [Display(Name = "Car Brand")]
        [Required(ErrorMessage = "Car Brand is required.")]
        public int CarBrandId { get; set; }

        [Display(Name = "Car Model")]
        [Required(ErrorMessage = "Car Model is required.")]
        public string CarModel { get; set; }

        [Display(Name = "Car Year")]
        [Required(ErrorMessage = "Car Year is required.")]
        public int CarYear { get; set; }

        [Display(Name = "Car Capacity With Driver")]
        [Required(ErrorMessage = "Car Capacity is required.")]
        [Range(2,8,ErrorMessage = "Car capacity should be between {1} and {2}")]
        public int CarCapacity { get; set; }

        [Display(Name = "Car Description")]
        [Required(ErrorMessage = "Car Description is required.")]
        public string CarDescription { get; set; }
        public HttpPostedFileBase Image { get; set; }
        public List<SelectListItem> ListOfBookingStatus { get; set; }
        public List<SelectListItem> ListOfCarBrand { get; set; } 
    }
}