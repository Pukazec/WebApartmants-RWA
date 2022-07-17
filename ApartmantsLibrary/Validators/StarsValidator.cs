using ApartmantsLibrary.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCApartmants.Models.Validators
{
    public class StarsValidator : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            int maxValue = 5;
            int minValue = 1;
            ApartmantReview stars = (ApartmantReview)validationContext.ObjectInstance;
            if (stars.Stars <= maxValue & stars.Stars >= minValue) return ValidationResult.Success;
            else return new ValidationResult($"Value can be between 1 and 5");
        }
    }
}