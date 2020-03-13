using Microsoft.AspNet.Identity.Owin;
using System;
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

    public class HomeController : VeronaBaseController
    {
        private IOrdersService ordersService;
        private IToursService toursService;

        public HomeController(
            IMainService newMainService,
            IOrdersService newOrdersService,
            IToursService newToursService,
            IIdentityService identityService) : base(newMainService, identityService)
        {
            toursService = newToursService;
            ordersService = newOrdersService;

            var user = System.Web.HttpContext.Current.User;
            if (user != null && user.Identity.IsAuthenticated)
            {
                var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var userDto = identityService.GetUserByEmail(user.Identity.Name, userManager);

                this.ViewData["TotalPrice"] = Convert.ToInt32(ordersService.GetTotalPriceForNotRegisterOrdersForUser(userDto.Id) * GetSale());
            }
            else
            {
                this.ViewData["TotalPrice"] = 0;
            }
        }

        [AllowAnonymous]
        public ActionResult Index(string status = "")
        {
            ViewData["alert"] = status;

            var tours = toursService.GetHotTours();

            return View(tours);
        }

        [AllowAnonymous]
        public ActionResult About()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Contact()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Tours(ToursViewModel model)
        {
            var tours = toursService.GetSortTours().ToList();
            var allTours = toursService.GetTours();

            if (model.FilterOptions != null)
            {
                tours = toursService.GetFilteredTours(model.FilterOptions).ToList();
            }

            int pageSize = 3;
            int page = 1;

            if (model.FilterOptions != null)
            {
                page = model.FilterOptions.SelectedPage;
                model.FilterOptions.SelectedPage = 1;
            }

            var feedingTypes = mainService.GetFeedingTypes().ToList();
            var toursPerPages = tours.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo 
            {
                PageNumber = page, 
                PageSize = pageSize, 
                TotalItems = tours.Count 
            };

            var max = 0;
            var min = 0;
            var maxCountOfPeople = 0;
            var selectedPrice = 0;

            if (allTours.Count() != 0)
            {
                max = allTours.Max(t => t.Price);
                min = allTours.Min(t => t.Price);
                maxCountOfPeople = allTours.Max(t => t.MaxPeopleCount);
                selectedPrice = allTours.Max(t => t.Price);
            }

            var toursViewModel = new ToursViewModel
            {
                Tours = toursPerPages,
                Countries = mainService.GetCountries(),
                TourTypeTitles = mainService.GetTourTypesTitles(),
                FeedingTypes = feedingTypes,
                MaxCountOfStartForHotel = mainService.GetMaxCountOfStarsForHotel(),
                MaxCountOfPeople = maxCountOfPeople,
                MaxPrice = max,
                MinPrice = min,
                StartDateTime = mainService.GetTommorowDate(),
                PageInfo = pageInfo,
                FilterOptions = new FilterDTO
                {
                    CheckedHotel = new List<bool>(new bool[mainService.GetMaxCountOfStarsForHotel()]),
                    CheckedFood = new List<bool>(new bool[feedingTypes.Count()]),
                    SelectedPrice = allTours.Max(t => t.Price),
                    SelectedNumberOfPeople = 0,
                    SelectedStartDate = mainService.GetTommorowDate(),
                    SelectedEndDate = mainService.GetInMonthDate()
                }
            };

            if (model.FilterOptions != null)
            {
                toursViewModel.FilterOptions = model.FilterOptions;
            }

            ViewData["MinDate"] = toursViewModel.FilterOptions.SelectedStartDate;
            ViewData["MaxDate"] = toursViewModel.FilterOptions.SelectedEndDate;
            return View(toursViewModel);
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Details(int id)
        {           
            var tour = toursService.GetTour(id);

            if (tour != null)
            { 
                var detailViewModel = new DetailsViewModel
                {
                    Tour = tour
                };

                return View(detailViewModel);
            }

            return HttpNotFound();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Details(DetailsViewModel detailsViewModel)
        {
            ordersService.CreateOrder(detailsViewModel.Tour.Id, currentUser.Id, detailsViewModel.NumberOfPeople);

            return RedirectToAction("Tours");
        }
        
        [HttpGet]
        public ActionResult Order()
        {
            
            var orders = ordersService.GetNotRegisterOrdersForUser(currentUser.Id);

            var notRegisteredOrder = new NotRegisteredOrdersViewModel
            {
                NotRegisteredOrder = orders.Select(order => new NotRegisteredOrderViewModel()
                {
                    OrderId = order.Id,
                    NumberOfPeople = order.NumberOfPeople,
                    Tour = order.Tour
                }),
                Sale = currentUser.Sale,
                TotalPrice = ordersService.GetTotalPriceForNotRegisterOrdersForUser(currentUser.Id)
            };
            return View(notRegisteredOrder);
        }
        
        [HttpPost]
        public ActionResult OrderPost()
        {
            
            var errors = ordersService.RegisterOrders(currentUser.Id);

            if (errors.Count() != 0)
            {
                AddErrors(errors);
                ViewData["OrderRegistrationStatus"] = "failure";
            }
            else
            {
                ViewData["OrderRegistrationStatus"] = "success";
                ViewData["TotalPrice"] = 0;
            }
            var orders = ordersService.GetNotRegisterOrdersForUser(currentUser.Id);

            var notRegisteredOrder = new NotRegisteredOrdersViewModel
            {
                NotRegisteredOrder = orders.Select(order => new NotRegisteredOrderViewModel()
                {
                    OrderId = order.Id,
                    NumberOfPeople = order.NumberOfPeople,
                    Tour = order.Tour
                }),
                Sale = currentUser.Sale,
                TotalPrice = ordersService.GetTotalPriceForNotRegisterOrdersForUser(currentUser.Id)
            };

            return View("Order", notRegisteredOrder);
        }
        
        public ActionResult DeleteOrder(int id)
        {
            ordersService.DeleteOrder(id);
            return RedirectToAction("Order");
        }
        
        [HttpGet]
        public ActionResult Profile()
        {            
            if(currentUser != null) ViewData["UserName"] = currentUser.Name;

            return View();
        }
       
        [HttpGet]
        public ActionResult EditProfile()
        {      
            var edit = new EditUserViewModel
            {
                Name = currentUser.Name,
                Phone = currentUser.PhoneNumber,
                Email = currentUser.Email,
                Password = currentUser.Password
            };

            return View(edit);
        }
        
        [HttpPost]
        public async Task<ActionResult> EditProfile(EditUserViewModel editUserViewModel)
        {
            if (ModelState.IsValid)
            {
                var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var signInManager = HttpContext.GetOwinContext().Get<ApplicationSignInManager>();

                var newUser = new UserDTO
                {
                    Email = editUserViewModel.Email,
                    Password = editUserViewModel.Password,
                    PhoneNumber = editUserViewModel.Phone,
                    Name = editUserViewModel.Name
                };

                var userEmail = currentUser.Email;

                await identityService.UpdateUser(userEmail, newUser, userManager, signInManager);

                return RedirectToAction("Profile");
            }

            return View();
        }
        
        [HttpGet]
        public ActionResult UserOrder()
        {
            
            var orders = ordersService.GetRegisterOrdersForUser(currentUser.Id);
            var registeredOrder = new OrdersViewModel
            {
                RegisteredOrder = orders,
                Sale = currentUser.Sale,
                TotalPrice = ordersService.GetTotalPriceForRegisterOrdersForUser(currentUser.Id)
            };
            return View(registeredOrder);
        }

        [AllowAnonymous]
        public ActionResult Error404()
        {
            Response.StatusCode = 404;
            return View();
        }

        [AllowAnonymous]
        public ActionResult Error403()
        {
            Response.StatusCode = 403;
            return View();
        }

        [AllowAnonymous]
        public ActionResult Error502()
        {
            Response.StatusCode = 502;
            return View();
        }
    }
}