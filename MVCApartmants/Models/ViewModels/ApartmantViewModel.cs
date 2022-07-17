using ApartmantsLibrary.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCApartmants.Models.ViewModels
{
    public class ApartmantViewModel
    {
        public Apartment Apartment { get; set; }
        public IList<TaggedApartmant> ApartmantTags { get; set; }
        public IList<ApartmantPicture> ApartmantPictures { get; set; }

        [Required(ErrorMessage = "User name is a must")]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Phone number is a must")]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Email is a must")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Adults number is a must")]
        public int ApartmantId { get; set; }

        [Required(ErrorMessage = "Adults number is a must")]
        [Range(1, 50, ErrorMessage = "Must be more than 0")]
        public int Adults { get; set; }
        [Required(ErrorMessage = "Children number is a must")]
        public int Children { get; set; }

        [Required(ErrorMessage = "Start date is a must")]
        public DateTime From { get; set; }

        [Required(ErrorMessage = "End date is a must")]
        public DateTime To { get; set; }
    }
}