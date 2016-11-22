using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Storyboard_App.Models;
using System.ComponentModel.DataAnnotations;

namespace Storyboard_App.Models
{
    public class Project
    {
        public int Id { get; set; }

        [Required]
        [UniqueNameValidation]
        public string Name { get; set; }

        //[EmptyListValidation]
        public List<Page> Pages { get; set; }

        public string Description { get; set; }

        //public Project()
        //{
        //    Pages = new List<Page>();
        //}
        
    }
}