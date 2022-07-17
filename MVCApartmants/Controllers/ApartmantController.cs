using ApartmantsLibrary.Dal;
using ApartmantsLibrary.Model;
using MVCApartmants.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCApartmants.Controllers
{
    public class ApartmantController : Controller
    {
        // GET: Apartmant
        [HttpGet]
        public ActionResult Index(int id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ApartmantViewModel viewModel = new ApartmantViewModel
            {
                Apartment = (System.Web.HttpContext.Current.Application["database"] as IRepo).LoadApartment(id),
                ApartmantTags = (System.Web.HttpContext.Current.Application["database"] as IRepo).LoadTaggedApartmant(id),
                From = DateTime.Now.Date,
                To = DateTime.Now.AddDays(1).Date
            };
            viewModel.Apartment.ImgUrl = "data:image/jpeg;base64," + viewModel.Apartment.ImgUrl;
            if (Session["user"] != null)
            {
                User RegisteredUser = (User)Session["user"];
                viewModel.UserName = RegisteredUser.UserName;
                viewModel.Email = RegisteredUser.Email;
                viewModel.PhoneNumber = RegisteredUser.PhoneNumber;
            }
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Index(ApartmantViewModel apartmantView)
        {
            if (!ModelState.IsValid)
            {
                return Json(false);
            }

            ApartmantReservation reservation = new ApartmantReservation();

            string details = $"Adults: {apartmantView.Adults}, Children {apartmantView.Children}, from: {apartmantView.From}, to: {apartmantView.To}";

            reservation.ApartmentId = apartmantView.ApartmantId;
            reservation.UserName = apartmantView.UserName;
            reservation.UserEmail = apartmantView.Email;
            reservation.UserPhone = apartmantView.PhoneNumber;
            reservation.Details = details;

           try
            {
                (System.Web.HttpContext.Current.Application["database"] as IRepo).AddReservationAnonimusUser(reservation);
            }
            catch (Exception)
            {

                throw;
            }

            return Json(true);
        }


        [HttpGet]
        public ActionResult Review(int id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ApartmantReview viewModel = new ApartmantReview
            {
                Apartment = (System.Web.HttpContext.Current.Application["database"] as IRepo).LoadApartment(id),
                Stars = 5
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Review(ApartmantReview review)
        {
            if (!ModelState.IsValid)
            {
                return Review(review.ApartmentId);
            }

            review.User = (User)Session["user"];
            review.UserId = review.User.IdUser;

            try
            {
                (System.Web.HttpContext.Current.Application["database"] as IRepo).AddReview(review);
                int apartmantId = review.ApartmentId;
                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult ApartmantPicture(int id)
        {
            ApartmantPicture picture = (System.Web.HttpContext.Current.Application["database"] as IRepo).LoadPicture(id);

            picture.Bytes = "data:image/jpeg;base64," + picture.Bytes;

            return View(picture);
        }

        public ActionResult Search()
        {
            return View();
        }
    }
}