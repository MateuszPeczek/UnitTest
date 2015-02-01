using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TestProject.Models;
using TestProject.Repository;
using TestProject.ServiceLayer;

namespace TestProject.Controllers
{
    public class CRUDController : Controller
    {
        IBlogPostService _BlogPostService;

        public CRUDController(IBlogPostService BlogPostService)
        {
            _BlogPostService = BlogPostService;
        }

        public ActionResult Index()
        {
            return View(_BlogPostService.SelectAll());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPost blogPost = _BlogPostService.GetById(id);
            if (blogPost == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            return View(blogPost);
        }

        public ActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BlogPost obj)
        {
            if (ModelState.IsValid)
            {
                _BlogPostService.Create(obj);
                return RedirectToAction("Index");
            }
            return View("Create", obj);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPost blogPost = _BlogPostService.GetById(id);
            if (blogPost == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            return View(blogPost);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult HttpPostEdit(BlogPost obj)
        {
            if (ModelState.IsValid)
            {
                _BlogPostService.Update(obj);
                return RedirectToAction("Index");
            }
            return View("Edit", obj);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPost blogPost = _BlogPostService.GetById(id);
            if (blogPost == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            return View(blogPost);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmDelete(int id, FormCollection data)
        {
            BlogPost blogPost = _BlogPostService.GetById(id);
            _BlogPostService.Delete(blogPost);
            return RedirectToAction("Index");
        }
    }
}
