using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Storyboard_App.Models;
using Storyboard_App.ViewModels;

namespace Storyboard_App.Controllers
{
    public class ProjectsController : Controller
    {
        private ApplicationDbContext _context;

        public ProjectsController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Project
        public ActionResult Index()
        {
            var projects = _context.Projects.ToList();
            return View(projects);
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        //Project Actions
        [Route("projects/display/{project}")]
        public ActionResult DisplayProject(string project)
        {
            if (!string.IsNullOrWhiteSpace(project))
            {
                var projectInDb = _context.Projects.Single(c => c.Name == project);

                if (projectInDb != null)
                {                    
                    return View(projectInDb);
                }
            }

            return RedirectToAction("Index");
        }

        public ActionResult NewProject()
        {
            Project project = new Project();
            //project.Pages = new List<Page>();

            return View("ProjectForm", project);
        }

        public ActionResult EditProject(int id)
        {
            var project = _context.Projects.SingleOrDefault(c => c.Id == id);

            if (project == null)
            {
                return HttpNotFound();
            }

            return View("ProjectForm", project);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveProject(Project project)
        {
            
            if (!ModelState.IsValid)
            {                
                return View("ProjectForm", project);
            }

            if (project.Id == 0)
            {
                _context.Projects.Add(project);
            }

            else
            {
                var projectInDb = _context.Projects.Single(c => c.Id == project.Id);

                projectInDb.Name = project.Name;
                projectInDb.Description = project.Description;

            }
            _context.SaveChanges();

            return RedirectToAction("DisplayProject","Projects",new { project = project.Name });
        }


        //Page actions
        [Route("projects/display/{project}/{page}")]
        public ActionResult DisplayPage(string project, int page)
        {
            if (!string.IsNullOrWhiteSpace(project))
            {
                var projectInDb = _context.Projects.Single(c => c.Name == project);

                if (projectInDb != null)
                {
                    var pageId = projectInDb.Pages.SingleOrDefault(c => c.Num == page);

                    if (pageId != null)
                    {
                        var pageInDb = _context.Pages.Single(c => c.Id == pageId.Id);

                        if (pageInDb != null)
                        {                            
                            return View("PageDisplay", pageInDb);
                        }
                    }

                    return View("ProjectDisplay", projectInDb);
                }
            }

            return RedirectToAction("Projects");
        }

        public ActionResult NewPage(string project)
        {
            if (!string.IsNullOrWhiteSpace(project))
            {
                var projectInDb = _context.Projects.Single(c => c.Name == project);

                if (projectInDb != null)
                {                    
                    return View("PageForm", new Page());

                }
            }
            return HttpNotFound();
        }

        public ActionResult EditPage(int id, Project project)
        {
            if (project == null)
            {
                return HttpNotFound();
            }

            var page = project.Pages.Single(c => c.Id == id);

            if (page == null)
            {
                return HttpNotFound();
            }

            var viewModel = new PageViewModel
            {
                Project = project,
                Page = page
            };
            return View("PageForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SavePage(Page page)
        {
            if (!ModelState.IsValid)
            {
                return View("PageForm", page);
            }

            if (page.Id == 0)
            {
                var projectInDb = _context.Projects.Single(c => c.Id == page.ProjectId);
                _context.Pages.Add(page);
                if (projectInDb.Pages == null)
                {
                    projectInDb.Pages = new List<Page>();
                }
                projectInDb.Pages.Add(page);
            }
            else
            {
                var pageInDb = _context.Pages.Single(c => c.Id == page.Id);

                pageInDb.Num = page.Num;
                pageInDb.NotesSketch = page.NotesSketch;
                pageInDb.RoughSketch = page.RoughSketch;
                pageInDb.FinalSketch = page.FinalSketch;
                pageInDb.Description = page.Description;
                pageInDb.Script = page.Script;

            }
            _context.SaveChanges();

            return RedirectToAction("DisplayPage", "Projects", new { page = page.Num });
        }

        public ActionResult test()
        {
           return View();
        }
    }
}