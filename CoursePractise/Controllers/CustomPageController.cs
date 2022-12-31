using CoursePractise.Data;
using CoursePractise.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace CoursePractise.Controllers
{
    [Authorize(Roles = WebConstants.AdminRole)]
    public class CustomPageController : Controller
    {

        private  readonly ApplicationDbContext _db;
        public CustomPageController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: CustomPageController
        public ActionResult Index()
        {
            IEnumerable<CustomPage> objList = _db.CustomPage;
            return View(objList);
        }


        // GET: CustomPageController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CustomPageController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CustomPage obj)
        {
            if (ModelState.IsValid)
            {
                _db.CustomPage.Add(obj);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
           return View(obj);
        }

        // GET: CustomPageController/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == 0) NotFound();
            var obj = _db.CustomPage.Find(id);
            if (obj == null) NotFound();
            return View(obj);
        }

        // POST: CustomPageController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CustomPage obj)
        {
            _db.CustomPage.Update(obj);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // GET: CustomPageController/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == 0) NotFound();
            var obj = _db.CustomPage.Find(id);
            if (obj == null) NotFound();
            return View(obj);
        }

        // POST: CustomPageController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(CustomPage obj)
        {
            _db.CustomPage.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
