using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CarRentingSystem.ViewModel
{
    public class BookingViewModel
    {
        [Display(Name = "Customer Name")]
        [Required(ErrorMessage ="Customer Name is required")]
        public string CustomerName { get; set; }

        [Display(Name = "Customer Address")]
        [Required(ErrorMessage = "Customer Address is required")]
        public string CustomerAddress { get; set; }

        [Display(Name = "Customer Phone Number")]
        [Required(ErrorMessage = "Customer Phone Number is required")]
        public string CustomerPhoneNumber { get; set; }

        [Display(Name = "Booking From")]
        [Required(ErrorMessage = "Booking From is required")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime BookingFrom { get; set; }

        [Display(Name = "Booking To")]
        [Required(ErrorMessage = "Booking To is required")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime BookingTo { get; set; }

        [Display(Name = "Assign Car")]
        [Required(ErrorMessage = "Assigned Car is required")]
        public int AssignCarId { get; set; }

        [Display(Name = "Number of Members")]
        [Required(ErrorMessage = "Number of Members is required")]
        public int  NumberOfMembers{ get; set; }
        public IEnumerable<SelectListItem> ListOfCars { get; set; }
    }
}