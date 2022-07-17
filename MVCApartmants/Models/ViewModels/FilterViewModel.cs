using ApartmantsLibrary.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCApartmants.Models.ViewModels
{
    public class FilterViewModel
    {
        public enum PriceE
        {
            Select,
            [Display(Name = "Price asc")]
            PriceAsc,
            [Display(Name = "Price desc")]
            PriceDesc,
        }
        public int Rooms { get; set; }
        public int Adults { get; set; }
        public int Children { get; set; }
        public IList<City> Cities { get; set; }
        public City SelectedCity { get; set; }
        public PriceE Price { get; set; }
    }
}