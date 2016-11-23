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

            if (project.Name == @";newProject")
            {
                return new ValidationResult("Illegal name");
            }

            bool HasId = (project.Id == 0) ? false : true;
            foreach(var p in _context.Projects)
            {
                if (p.Name == project.Name && !HasId)
                {
                    return new ValidationResult("Name must be unique");
                }
            }
            return ValidationResult.Success;
        }
    }
}