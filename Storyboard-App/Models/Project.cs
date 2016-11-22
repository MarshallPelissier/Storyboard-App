using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Storyboard_App.Models;

namespace Storyboard_App.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Page> Pages { get; set; }
        public string Description { get; set; }
    }
}