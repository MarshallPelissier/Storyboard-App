﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Storyboard_App.Models;
using Storyboard_App.ViewModels;
using System.Data.Entity;

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
                var projectInDb = _context.Projects.Include(c=>c.Pages).Single(c => c.Name == project);

                if (projectInDb != null)
                {                    
                    return View(projectInDb);
                }
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult NewProject()
        {
            Project project = new Project();

            return PartialView("_ProjectForm", project);
        }

        [HttpGet] // this action result returns the partial containing the modal
        public ActionResult EditProject(int id)
        {
            var project = _context.Projects.SingleOrDefault(c => c.Id == id);

            if (project == null)
            {
                return HttpNotFound();
            }

            return PartialView("_ProjectForm", project);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveProject(Project project)
        {
            bool changed = false;
            string name = "";
            if (!ModelState.IsValid)
            {
                return PartialView("_ProjectForm", project);
            }

            if (project.Id == 0)
            {
                _context.Projects.Add(project);
                name = project.Name;
                changed = true;
            }

            else
            {
                var projectInDb = _context.Projects.Single(c => c.Id == project.Id);
                if (project.Name != projectInDb.Name || project.Description != projectInDb.Description)
                {
                    changed = true;
                    name = project.Name;
                }
                projectInDb.Name = project.Name;
                projectInDb.Description = project.Description;

            }
            _context.SaveChanges();
            if (changed)
            {
                return PartialView("_FormSuccessRedirect",name);
            }
            return PartialView("_FormSuccess",project);
        }

        //Page actions
        [Route("projects/display/{project}/{page}")]
        public ActionResult DisplayPage(string project, int page)
        {
            if (!string.IsNullOrWhiteSpace(project))
            {
                var projectInDb = _context.Projects.Include(c => c.Pages).Single(c => c.Name == project);
                
                if (projectInDb != null)
                {
                    var pageId = projectInDb.Pages.SingleOrDefault(c => c.Num == page);

                    if (pageId != null)
                    {
                        var pageInDb = _context.Pages.Single(c => c.Id == pageId.Id);

                        if (pageInDb != null)
                        {                            
                            return View("DisplayPage", pageInDb);
                        }
                    }

                    return View("ProjectDisplay", projectInDb);
                }
            }

            return RedirectToAction("Projects");
        }

        [HttpGet]
        [Route("projects/newpage/{project}")]
        public ActionResult NewPage(string project)
        {
            if (!string.IsNullOrWhiteSpace(project))
            {
                var projectInDb = _context.Projects.Single(c => c.Name == project);
                var page = new Page();
                page.ProjectId = projectInDb.Id;

                if (projectInDb != null)
                {                    
                    return PartialView("_PageForm", page);

                }
            }
            return HttpNotFound();
        }

        [HttpGet]
        [Route("projects/editpage/{project}/{num}")]
        public ActionResult EditPage( string project, int num)
        {
            if (string.IsNullOrWhiteSpace(project))
            {
                return HttpNotFound();
            }
            var projectInDb = _context.Projects.Include(c=>c.Pages).Single(c => c.Name == project);
            if (project == null)
            {
                return HttpNotFound();
            }

            var page = projectInDb.Pages.Single(c => c.Num == num);

            if (page == null)
            {
                return HttpNotFound();
            }

            var viewModel = new PageViewModel
            {
                Project = projectInDb,
                Page = page
            };
            return PartialView("_PageForm", page);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SavePage(Page page)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_PageForm", page);
            }

            if (page.Id == 0)
            {
                var projectInDb = _context.Projects.Include(c => c.Pages).Single(c => c.Id == page.ProjectId);
                _context.Pages.Add(page);
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

            return PartialView("_FormSuccess");
        }
        
        public ActionResult OrderPages(string project, int? pageNum)

        {
            if (!string.IsNullOrWhiteSpace(project))
            {
                var projectInDb = _context.Projects.Include(c => c.Pages).Single(c => c.Name == project);

                if (projectInDb != null)
                {
                    Page page;
                    if (pageNum != null)
                    {
                        page = projectInDb.Pages.Single(p => p.Num == pageNum);
                    }
                    else
                    {
                        page = null;
                    }
                    projectInDb.Pages.Sort((x, y) => x.Num.CompareTo(y.Num));
                    var viewModel = new PageViewModel
                    {
                        Project = projectInDb,
                        Page = page
                    };
                    return View(viewModel);
                }
            }
            return HttpNotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult SaveOrder(Project project, int? id)
        public ActionResult SaveOrder(Project project)
        {
            //if (!string.IsNullOrWhiteSpace(project))
            //{
            //    var projectInDb = _context.Projects.Include(c => c.Pages).Single(c => c.Name == project);
            //    if (projectInDb != null)
            //    {
            //        var pagesInDb = _context.Pages;
            //        foreach (var page in pagesInDb)
            //        {
            //            projectInDb.Pages.Single(p => p.Id == page.Id).Num = page.Num;
            //        }
            //        _context.SaveChanges();
            //        Page pageInDb = null;
            //        if (id != null)
            //        {
            //            pageInDb = projectInDb.Pages.Single(p => p.Id == id);

            //            return RedirectToAction("DisplayProject", new { project = projectInDb.Name, page = pageInDb.Num });
            //        }

            //        return RedirectToAction("DisplayProject", new { project = projectInDb.Name });
            //    }
            //}
            //return HttpNotFound();
            var projectInDb = _context.Projects.Include(c => c.Pages).Single(c => c.Id == project.Id);
            if (projectInDb != null)
            {
                var pagesInDb = projectInDb.Pages;
                foreach (var page in pagesInDb)
                {
                    projectInDb.Pages.Single(p => p.Id == page.Id).Num = project.Pages.Single(p=>p.Id ==page.Id).Num;
                }
                projectInDb.Pages.Sort((x, y) => x.Num.CompareTo(y.Num));
                _context.SaveChanges();
                //Page pageInDb = null;
                //if (id != null)
                //{
                //    pageInDb = projectInDb.Pages.Single(p => p.Id == id);

                //    return RedirectToAction("DisplayProject", new { project = projectInDb.Name, page = pageInDb.Num });
                //}

                return RedirectToAction("DisplayProject", new { project = projectInDb.Name });
            }
            return HttpNotFound();
        }
    }
}