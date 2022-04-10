using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarRentingSystem.Models;
using CarRentingSystem.ViewModel;

namespace CarRentingSystem.Controllers
{
    public class BookingController : Controller
    {
        private CarDBEntities objCarDBEntities;
        public BookingController()
        {
            objCarDBEntities = new CarDBEntities();
        }
        public ActionResult Index()
        {
            BookingViewModel objBookingViewModel = new BookingViewModel();
            objBookingViewModel.ListOfCars = (from objCar in objCarDBEntities.Cars
                                              where objCar.BookingStatusId == 2
                                              select new SelectListItem()
                                              {
                                                  Text = objCar.CarNumber,
                                                  Value = objCar.CarId.ToString()
                                              }
                                           ).ToList();
            objBookingViewModel.BookingFrom = DateTime.Now;
            objBookingViewModel.BookingTo = DateTime.Now.AddDays(1);
            return View(objBookingViewModel);
        }

        [HttpPost]
        public ActionResult Index(BookingViewModel objBookingViewModel)
        {
            //string dt = objBookingViewModel.BookingTo.ToString("dd/MM/yyyy");
            int numberOfDays = Convert.ToInt32((objBookingViewModel.BookingTo - objBookingViewModel.BookingFrom).TotalDays);
            Car objCar = objCarDBEntities.Cars.Single(model => model.CarId == objBookingViewModel.AssignCarId);
            decimal CarPrice = objCar.CarPrice;
            decimal TotalAmount = CarPrice * numberOfDays;

            CarBooking carBooking = new CarBooking()
            {
                BookingFrom = objBookingViewModel.BookingFrom,
                BookingTo = objBookingViewModel.BookingTo,
                AssignCarId = objBookingViewModel.AssignCarId,
                CustomerAddress = objBookingViewModel.CustomerAddress,
                CustomerName = objBookingViewModel.CustomerName,
                CustomerPhoneNumber = objBookingViewModel.CustomerPhoneNumber,
                NoOfMembers = objBookingViewModel.NumberOfMembers,
                TotalAmount = TotalAmount
            };
            objCarDBEntities.CarBookings.Add(carBooking);
            objCarDBEntities.SaveChanges();

            objCar.BookingStatusId = 3;
            objCarDBEntities.SaveChanges();

            return Json(new { message = "Car Booking is Successfully Created.", success = true}, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public PartialViewResult GetAllBookingHistory() 
        {
            List<CarBookingViewModel> listOfBookingViewModels = new List<CarBookingViewModel>();
            listOfBookingViewModels = (from CarBooking in objCarDBEntities.CarBookings
                                       join objCar in objCarDBEntities.Cars on CarBooking.AssignCarId equals objCar.CarId
                                       select new CarBookingViewModel()
                                       {
                                           BookingFrom = CarBooking.BookingFrom,
                                           BookingTo = CarBooking.BookingTo,
                                           CustomerPhoneNumber = CarBooking.CustomerPhoneNumber,
                                           CustomerName = CarBooking.CustomerName,
                                           TotalAmount = CarBooking.TotalAmount,
                                           CustomerAddress = CarBooking.CustomerAddress,
                                           NumberOfMembers = CarBooking.NoOfMembers,
                                           BookingId = CarBooking.BookingId,
                                           CarNumber = objCar.CarNumber,
                                           CarPrice = objCar.CarPrice,
                                           NumberOfDays = System.Data.Entity.DbFunctions.DiffDays(CarBooking.BookingFrom, CarBooking.BookingTo).Value
                                       }).ToList();
            return PartialView("_BookingHistoryPartial",listOfBookingViewModels);
        }
    }
}