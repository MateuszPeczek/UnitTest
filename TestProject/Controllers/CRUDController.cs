using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestProject.Models;
using TestProject.Repository;

namespace TestProject.Controllers
{
    public class CRUDController : Controller
    {
        private IBlogRepository _repository = null;
        public CRUDController()
        {
            _repository = new BlogRepository();
        }
        public CRUDController(IBlogRepository repository)
        {
            _repository = repository;
        }

        public ActionResult Index()
        {
            return View(_repository.SelectAll().ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(400);
            }
            BlogPost existing = _repository.SelectByID(id);
            if (existing == null)
            {
                return new HttpStatusCodeResult(404);
            }
            return View(existing);
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
                _repository.Create(obj);
                _repository.Save();
                return RedirectToAction("Index");
            }
            return View("Create", obj);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(400);
            }
            BlogPost existing = _repository.SelectByID(id);
            if (existing == null)
            {
                return new HttpStatusCodeResult(404);
            }
            return View(existing);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult HttpPostEdit(BlogPost obj)
        {
            if (ModelState.IsValid)
            {
                _repository.Update(obj);
                _repository.Save();
                return RedirectToAction("Index");
            }
            return View("Edit", obj);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(400);
            }
            BlogPost existing = _repository.SelectByID(id);
            if (existing == null)
            {
                return new HttpStatusCodeResult(404);
            }
            return View(existing);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmDelete(int? id)
        {
            if (ModelState.IsValid)
            {
                _repository.Delete(id);
                _repository.Save();
                return RedirectToAction("Index");
            }
            return View("Delete", id);
        }
    }
}
