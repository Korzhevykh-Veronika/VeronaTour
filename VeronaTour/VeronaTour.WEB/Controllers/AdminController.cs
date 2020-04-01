using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using NLog;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using VeronaTour.BLL.DTOs;
using VeronaTour.BLL.Services;
using VeronaTour.BLL.Services.Interfaces;
using VeronaTour.BLL.Utils;
using VeronaTour.WEB.Models;

namespace VeronaTour.WEB.Controllers
{
    [Authorize(Roles = "Admin, Manager")]
    public class AdminController : VeronaBaseController
    {
        private IOrdersService ordersService;
        private IToursService toursService;

        public AdminController(
            IMainService newMainService,
            IOrdersService newOrdersService,
            IToursService newToursService,
            IIdentityService identityService,
            ILogger logger) : base(newMainService, identityService, logger)
        {
            toursService = newToursService;
            ordersService = newOrdersService;

            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = identityService.GetUserByEmail(
                System.Web.HttpContext.Current.User.Identity.Name,
                userManager);

            if(user != null) ViewData["UserName"] = user.Name;
        }
        
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult Orders()
        {
            var orders = ordersService.GetAllOrders();
            var allOrder = new OrdersViewModel
            {
                RegisteredOrder = orders
            };
            return View(allOrder);
        }

        [HttpPost]
        public ActionResult ChangeHot(int tourId)
        {
            toursService.ChangeTourHot(tourId);           
            return RedirectToAction("Tours");
        }

        public ActionResult DetailsOrder(int id)
        {
            var order = ordersService.GetOrder(id);

            if(order != null)
            {
                return View(order);
            }

            return HttpNotFound();
        }
        
        public ActionResult Tours()
        {
            var tours = toursService.GetSortAdminTours();

            return View(tours);
        }
        
        [Authorize(Roles = "Admin")]
        public ActionResult DetailsTour(int id)
        {
            var tour = toursService.GetTour(id, true);

            if (tour != null)
            {
                return View(tour);
            }

            return HttpNotFound();
            
        }
        
        [Authorize(Roles = "Admin")]
        public ActionResult Hotels()
        {
            var hotels = mainService.GetHotels();

            return View(hotels);
        }
        
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Users()
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var newUsers = await identityService.GetUsers(userManager);

            var model = new UsersViewModel()
            {
                Users = newUsers,
                Roles = await identityService.GetRoles(),
                IsBlocked = ""
            };

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> UpdateUserSettings(UsersViewModel model, string email)
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var errors = new List<string>();

            if (email != currentUser.Email)
                await identityService.UpdateUserSettings(email, model.SelectedRole, model.IsBlocked, userManager);
            else
                errors.Add("You cannot update yourself.");

            if (errors.Any())
            {
                AddErrors(errors);                
            }

            var users = await identityService.GetUsers(userManager);

            var newModel = new UsersViewModel()
            {
                Users = users,
                Roles = await identityService.GetRoles(),
                IsBlocked = ""
            };

            return View("Users", newModel);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult CreateTour()
        {
            var addTour = InitialteTourViewModel();

            return View(addTour);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult CreateTour(AddTourViewModel model)
        {
            if (ModelState.IsValid)
            {
                var errors = toursService.AddTour(
                    model.Tour,
                    model.SelectedHotelTitle,
                    model.SelectedFeedingType,
                    model.SelectedTourType,
                    model.SelectedCountry,
                    model.uploadImage);

                if (errors.Count() == 0)
                {
                    return RedirectToAction("Tours");
                }

                AddErrors(errors);
            }

            var addTour = InitialteTourViewModel();
            addTour.Tour = model.Tour;

            return View(addTour);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult CreateHotel()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult CreateHotel(HotelDTO hotel)
        {
            if (ModelState.IsValid)
            {
                var errors = mainService.AddHotel(hotel);

                if (errors.Count() == 0)
                {
                    return RedirectToAction("Hotels");
                }
                AddErrors(errors);
            }

            return View(hotel);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult AddCountry()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult AddCountry(CountryDTO country)
        {
            if (ModelState.IsValid)
            {
                var errors = mainService.AddCountry(country);

                if (errors.Count() == 0)
                {
                    return RedirectToAction("CreateTour");
                }

                AddErrors(errors);
            }
            return View(country);

        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult AddTourType()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult AddTourType(TourTypeDTO tourType)
        {
            if (ModelState.IsValid)
            {
                var errors = mainService.AddTourType(tourType);

                if (errors.Count() == 0)
                {
                    return RedirectToAction("CreateTour");
                }

                AddErrors(errors);
            }

            return View(tourType);

        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult EditTour(int id)
        {
            var tour = toursService.GetTour(id, true);

            if (tour != null)
            {
                var model = InitialteTourViewModel();
                model.Tour = tour;
                model.SelectedHotelTitle = tour.Hotel.Title + " * " + tour.Hotel.StarsCount;
                model.SelectedFeedingType = tour.FeedingType.Title;
                model.SelectedCountry = tour.Country.Title;
                model.SelectedTourType = tour.Type.Title;

                ViewData["MinDate"] = tour.StartDate.ToString("MM/dd/yyyy");
                ViewData["MaxDate"] = tour.EndDate.ToString("MM/dd/yyyy");

                return View(model);

            }
            return HttpNotFound();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult EditTour(AddTourViewModel model)
        {
            if (ModelState.IsValid)
            {
                var errors = toursService.EditTour(
                  model.Tour,
                  model.SelectedHotelTitle,
                  model.SelectedFeedingType,
                  model.SelectedTourType,
                  model.SelectedCountry,
                  model.uploadImage);

                if (errors.Count() == 0)
                {
                    return RedirectToAction("Tours");
                }

                AddErrors(errors);
            }

            var newModel = InitialteTourViewModel();
            newModel.Tour = model.Tour;

            return View(newModel);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult EditHotel(int id)
        {
           var hotel = mainService.GetHotel(id);

            if (hotel != null)
            {
                return View(hotel);
            }

            return HttpNotFound();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult EditHotel(HotelDTO model)
        {
            if (ModelState.IsValid)
            {
                var errors = mainService.EditHotel(model);

                if (errors.Count() == 0)
                {
                    return RedirectToAction("Hotels");
                }

                AddErrors(errors);
            }

            return View(model);
        }

        [Authorize(Roles = "Admin, Manager")]
        public async Task<ActionResult> ChangeOrderStatus(int orderId, string status)
        {
            ordersService.ChangeOrderStatus(orderId, status);

            if (status == "Paid")
            {
                var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var signInManager = HttpContext.GetOwinContext().Get<ApplicationSignInManager>();

                await identityService.RecalculateSaleByOrder(
                    orderId,
                    userManager,
                    signInManager);
            }

            return RedirectToAction("Orders");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult DeleteTour(int id)
        {
            toursService.DeleteTour(id);
           return RedirectToAction("Tours");
            
        }

        [Authorize(Roles = "Admin")]
        public ActionResult DeleteHotel(int id)
        {
            var errors = mainService.DeleteHotel(id);

            if(errors.Count() != 0)
            {
                AddErrors(errors);

                var hotels = mainService.GetHotels();
                return View("Hotels", hotels);

            }

            return RedirectToAction("Hotels");
        }
        
        [HttpGet]
        public ActionResult Sale()
        {
            var currentSettings = ordersService.GetSaleSettings();

            return View(currentSettings);
        }

        [HttpPost]
        public ActionResult Sale(SaleSettingsDTO model)
        {
            if (ModelState.IsValid)
            {
                 ordersService.AddSale(model);                
                 return RedirectToAction("Index");
            }

            return View();
        }

        public ActionResult Logs()
        {
            var logs = mainService.GetLogs();
            return View(logs);
        }

        private AddTourViewModel InitialteTourViewModel()
        {
            var addTour = new AddTourViewModel
            {
                Tour = new TourDTO(),
                Countries = mainService.GetCountries(),
                TourTypeTitles = mainService.GetTourTypesTitles(),
                FeedingTypes = mainService.GetFeedingTypesTitles(),
                HotelTitles = mainService.GetHotelsTitles()
            };
            ViewData["MinDate"] = mainService.GetTommorowDate();
            ViewData["MaxDate"] = mainService.GetIn2WeeksDate();

            return addTour;
        }
        
    }
}