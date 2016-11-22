using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Storyboard_App.Models
{
    public class UniqueNameValidation : ValidationAttribute
    {
        private ApplicationDbContext _context;

        public UniqueNameValidation()
        {
            _context = new ApplicationDbContext();
        }
        
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var project = (Project)validationContext.ObjectInstance;
            foreach(var p in _context.Projects)
            {
                if (p.Name == project.Name)
                {
                    return new ValidationResult("Name must be unique");
                }
            }
            return ValidationResult.Success;
        }
    }
}