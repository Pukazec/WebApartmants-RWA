using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ApartmantsLibrary.Model
{
    public class User : IUser
    {
        public int IdUser { get; set; }
        public string Id { get; set; }
        public Guid Guid { get; set; }

        [Required(ErrorMessage = "User name is a must")]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool IsAdmin { get; set; }

        public override string ToString() => $"{UserName}";
    }
}
