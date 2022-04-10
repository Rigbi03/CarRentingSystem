using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarRentingSystem.Models;
using CarRentingSystem.ViewModel;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace CarRentingSystem.Controllers
{
    public class CarController : Controller
    {
        private CarDBEntities objCarDbEntities;
        public CarController()
        {
            objCarDbEntities = new CarDBEntities();
        }
        public ActionResult Index()
        {
            CarViewModel objCarViewModel = new CarViewModel();
            objCarViewModel.ListOfBookingStatus = (from obj in objCarDbEntities.BookingStatus
                                                   select new SelectListItem()
                                                   {
                                                       Text = obj.BookingStatus,
                                                       Value = obj.BookingStatusId.ToString()
                                                   }).ToList();

            objCarViewModel.ListOfCarBrand = (from obj in objCarDbEntities.CarBrands
                                              select new SelectListItem()
                                              {
                                                  Text = obj.CarBrandName,
                                                  Value = obj.CarBrandId.ToString()
                                              }).ToList();
            return View(objCarViewModel);
        }
        //[Authorize]
        [HttpPost]
        public ActionResult Index(CarViewModel objCarViewModel)
        {
            string message = String.Empty;
            string ImageUniqueName = String.Empty;
            string ActualImageName = String.Empty;
            if (objCarViewModel.CarId == 0)
            {
                ImageUniqueName = Guid.NewGuid().ToString();
                ActualImageName = ImageUniqueName + Path.GetExtension(objCarViewModel.Image.FileName);

                objCarViewModel.Image.SaveAs(Server.MapPath("~/CarImages/" + ActualImageName));
                //objCarDbEntities
                Car objCar = new Car()
                {
                    CarNumber = objCarViewModel.CarNumber,
                    CarDescription = objCarViewModel.CarDescription,
                    CarPrice = objCarViewModel.CarPrice,
                    BookingStatusId = objCarViewModel.BookingStatusId,
                    IsActive = true,
                    CarImage = ActualImageName,
                    CarCapacity = objCarViewModel.CarCapacity,
                    CarBrandId = objCarViewModel.CarBrandId,
                    CarModel = objCarViewModel.CarModel,
                    CarYear = objCarViewModel.CarYear,
                    CarId = objCarViewModel.CarId
                };
                objCarDbEntities.Cars.Add(objCar);
                message = "Added.";
            }
            else
            {
                Car objCar = objCarDbEntities.Cars.Single(model => model.CarId == objCarViewModel.CarId);

                if (objCarViewModel.Image != null) 
                {
                    ImageUniqueName = Guid.NewGuid().ToString();
                    ActualImageName = ImageUniqueName + Path.GetExtension(objCarViewModel.Image.FileName);
                    objCarViewModel.Image.SaveAs(Server.MapPath("~/CarImages/" + ActualImageName));
                    objCar.CarImage = ActualImageName;

                }
                objCar.CarNumber = objCarViewModel.CarNumber;
                objCar. CarDescription = objCarViewModel.CarDescription;
                objCar.CarPrice = objCarViewModel.CarPrice;
                objCar.BookingStatusId = objCarViewModel.BookingStatusId;
                objCar.IsActive = true;
                objCar.CarCapacity = objCarViewModel.CarCapacity;
                objCar.CarBrandId = objCarViewModel.CarBrandId;
                objCar.CarModel = objCarViewModel.CarModel;
                objCar.CarYear = objCarViewModel.CarYear;
                objCar.CarId = objCarViewModel.CarId;
                message = "Updated.";
            }

            objCarDbEntities.SaveChanges();
            return Json(data: new { message = "Car Successfully " + message, success = true }, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult GetAllCars()
        {
            IEnumerable<CarDetailsViewModel> listOfCarDetailsViewModels =
                (from objCar in objCarDbEntities.Cars
                    join objBooking in objCarDbEntities.BookingStatus on objCar.BookingStatusId equals objBooking.BookingStatusId
                    join objCarBrand in objCarDbEntities.CarBrands on objCar.CarBrandId equals objCarBrand.CarBrandId
                 where objCar.IsActive == true
                 select new CarDetailsViewModel()
                 {
                     CarNumber = objCar.CarNumber,
                     CarDescription = objCar.CarDescription,
                     CarCapacity = objCar.CarCapacity,
                     CarPrice = objCar.CarPrice,
                     BookingStatus = objBooking.BookingStatus,
                     CarBrand = objCarBrand.CarBrandName,
                     CarImage = objCar.CarImage,
                     CarId = objCar.CarId,
                     CarModel = objCar.CarModel,
                     CarYear = objCar.CarYear
                 }).ToList();
            return PartialView("_CarDetailsPartial", listOfCarDetailsViewModels);
        }

        //[Authorize]
        [HttpGet]
        public JsonResult EditCarDetails(int carId)
        {
            var result = objCarDbEntities.Cars.Single(model => model.CarId == carId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        //[Authorize]
        [HttpGet]
        public JsonResult DeleteCarDetails(int carId)
        { 
            Car objCar = objCarDbEntities.Cars.Single(model => model.CarId == carId);
            objCar.IsActive = false;
            objCarDbEntities.SaveChanges();
            return Json(new { message = "Record Successfully Deleted.", success = true }, JsonRequestBehavior.AllowGet);
        }
    }
}