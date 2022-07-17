using ApartmantsLibrary.Model;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCApartmants.Models.ViewModels
{
    public class RegisterViewModel : User
    {
        [Required(ErrorMessage = "Address is a must")]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "Email")]
        [EmailAddress]
        [Required(ErrorMessage = "Email is a must")]
        public string Email{ get; set; }

        [Compare("Password", ErrorMessage = "Passwords is a must")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Must confirm password")]
        [Compare("Password", ErrorMessage = "Passwords must match")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        public string ConfirmPassword { get; set; }
    }
}