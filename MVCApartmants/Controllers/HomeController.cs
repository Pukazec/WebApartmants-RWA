using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ApartmantsLibrary.Dal;
using ApartmantsLibrary.Model;
using MVCApartmants.Models;
using MVCApartmants.Models.ViewModels;

namespace MVCApartmants.Controllers
{
    public class HomeController : Controller
    {
        private IList<User> _allUsers;

        public ActionResult Index()
        {
            FilterViewModel filterViewModel = new FilterViewModel();
            filterViewModel.Cities = (System.Web.HttpContext.Current.Application["database"] as IRepo).LoadCities();
            return View(filterViewModel);
        }

        [HttpPost]
        public ActionResult Index(FilterViewModel filter)
        {
            HttpCookie cookieFilter = Response.Cookies["filter"];
            var selectedCity = filter.Cities;
            cookieFilter["rooms"] = filter.Rooms.ToString();
            cookieFilter["adults"] = filter.Adults.ToString();
            cookieFilter["children"] = filter.Children.ToString();
            cookieFilter["city"] = this.Request.Form["SelectedCity"];// filter.Cities.ToString();
            cookieFilter["sort"] = filter.Price.ToString();
            filter.Cities = (System.Web.HttpContext.Current.Application["database"] as IRepo).LoadCities();
            return View(filter);
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        [HttpGet]
        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogIn(User user)
        {
            if (ModelState.IsValid)
            {
                user.Password = Cryptography.HashPassword(user.Password);
                User authUser = (System.Web.HttpContext.Current.Application["database"] as IRepo).AuthenticateUser(user.UserName, user.Password);


                if (authUser == null)
                {
                    return LogIn();
                }
                else
                {
                    Session["user"] = authUser;
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return LogIn();
            }
        }

        [HttpGet]
        public ActionResult Logout()
        {
            Session.RemoveAll();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration(RegisterViewModel user)
        {
            IRepo repo = (System.Web.HttpContext.Current.Application["database"] as IRepo);
            _allUsers= repo.LoadUsers();

            if (ModelState.IsValid
                && _allUsers.FirstOrDefault(u => u.UserName == user.UserName) == null)
            {
                user.Password = Cryptography.HashPassword(user.Password);
                repo.AddUser(user);
                Session["user"] = user;

                return RedirectToAction("Index", "Home");
            }            
            else
            {
                return Registration();
            }
        }
    }
}