using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Storyboard_App.Models;
using Storyboard_App.ViewModels;

namespace Storyboard_App.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string project, int? page)
        {                       
            return View();
        }               
    }
}