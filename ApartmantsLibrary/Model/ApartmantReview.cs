using MVCApartmants.Models.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmantsLibrary.Model
{
    public class ApartmantReview
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public int ApartmentId { get; set; }
        public Apartment Apartment { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string Details { get; set; }

        [Required(ErrorMessage = "Stars are a must")]
        [StarsValidator]
        [Display(Name = "Stars")]
        public int Stars { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
