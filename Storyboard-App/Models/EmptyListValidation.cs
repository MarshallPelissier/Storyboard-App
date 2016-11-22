using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Storyboard_App.Models
{
    public class EmptyListValidation : ValidationAttribute
    {
        private ApplicationDbContext _context;

        public EmptyListValidation()
        {
            _context = new ApplicationDbContext();
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var project= (Project)validationContext.ObjectInstance;   
            if(project.Pages != null)
            {
                if (project.Pages.Count >= 0)
                {
                    return ValidationResult.Success;
                }
            }
            return new ValidationResult("ERROR incorrect project page data.");
        }
    }
}