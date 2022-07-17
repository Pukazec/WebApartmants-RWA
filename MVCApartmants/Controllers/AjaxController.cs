using ApartmantsLibrary.Dal;
using ApartmantsLibrary.Model;
using MVCApartmants.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static MVCApartmants.Models.ViewModels.FilterViewModel;

namespace MVCApartmants.Controllers
{
    public class AjaxController : Controller
    {
        // GET: Ajax
        public ActionResult GetApartmants()
        {
            IList<Apartment> apartments = new List<Apartment>();
            if (Request.Cookies["filter"] == null)
            {
                IList<Apartment> allApartments = (System.Web.HttpContext.Current.Application["database"] as IRepo).LoadApartments();
                foreach (Apartment apartment in allApartments)
                {
                    if(apartment.StatusId != 1)
                    {
                        apartment.ImgUrl = "data:image/jpeg;base64," + apartment.ImgUrl;
                        apartments.Add(apartment);
                    }
                }
            }
            else
            {
                HttpCookie filters = Request.Cookies["filter"];
                string rooms = filters["rooms"];
                string adults = filters["adults"];
                string children = filters["children"];
                string city = filters["city"];
                apartments = (System.Web.HttpContext.Current.Application["database"] as IRepo).LoadFilteredApartments(rooms, adults, children, city);
                foreach (Apartment apartment in apartments)
                {
                    apartment.ImgUrl = "data:image/jpeg;base64," + apartment.ImgUrl;
                }

                if (filters["sort"] == "PriceAsc")
                {
                    return PartialView("_Apartmant", apartments.OrderBy(a => a.Price));
                }
                else if (filters["sort"] == "PriceDesc")
                {
                    return PartialView("_Apartmant", apartments.OrderByDescending(a => a.Price));
                }
            }

            return PartialView("_Apartmant", apartments);
        }

        public ActionResult GetAutocompleteApartmants(string term)
        {
            IList<Apartment> apartments = new List<Apartment>();

            IList<Apartment> allApartments = (System.Web.HttpContext.Current.Application["database"] as IRepo).LoadApartments();
            foreach (Apartment apartment in allApartments)
            {
                if (apartment.StatusId != 1)
                {
                    apartments.Add(apartment);
                }
            }

            var find = apartments.Where(a => a.NameEng.ToLower().Contains(term.ToLower())).Select(a => new
            {
                label = a.NameEng,
                value = a.Id
            });

            return Json(find, JsonRequestBehavior.AllowGet);
        }

        // GET: Ajax
        public ActionResult GetPictures(int id)
        {
            IList<ApartmantPicture> ApartmantPictures = (System.Web.HttpContext.Current.Application["database"] as IRepo).LoadPictures(id);
            foreach (ApartmantPicture picture in ApartmantPictures)
            {
                picture.Bytes = "data:image/jpeg;base64," + picture.Bytes;
            }
            return PartialView("_ApartmantPictures", ApartmantPictures);
        }                
    }
}