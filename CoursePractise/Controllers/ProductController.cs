using CoursePractise.Data;
using CoursePractise.Models;
using CoursePractise.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace CoursePractise.Controllers
{
    [Authorize(Roles = WebConstants.AdminRole)]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> objList = _db.Product;
            foreach (var obj in objList)
            {
                obj.Category = _db.Category.FirstOrDefault(u => u.CategoryId == obj.ID);
                obj.CustomPage = _db.CustomPage.FirstOrDefault(u => u.Id == obj.ID);
            }

            return View(objList);
        }
        public IActionResult Create(int? id)
        {

            ProductViewModel productVM = new ProductViewModel()
            {
                Product = new Product(),
                CategorySelectList = _db.Category.Select(i => new SelectListItem
                {
                    Text = i.CategoryName,
                    Value = i.CategoryId.ToString()
                }),
                CustomPageSelectList = _db.CustomPage.Select(i => new SelectListItem
                {
                    Text = i.CustomName,
                    Value = i.Id.ToString()
                })
            };

            return View(productVM);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(ProductViewModel productVM)
        {

            var files = HttpContext.Request.Form.Files;
            string webRootPath = _webHostEnvironment.WebRootPath;


            //Creating
            string upload = webRootPath + WebConstants.ImagesPath;
            string fileName = Guid.NewGuid().ToString();
            string extension = Path.GetExtension(files[0].FileName);

            using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
            {
                files[0].CopyTo(fileStream);
            }

            productVM.Product.Image = fileName + extension;

            _db.Product.Add(productVM.Product);

            _db.SaveChanges();
            return RedirectToAction("Index");

        }
        //Get - edit
        public IActionResult Edit(int id)
        {
            ProductViewModel productVM = new ProductViewModel()
            {
                Product = new Product(),
                CategorySelectList = _db.Category.Select(i => new SelectListItem
                {
                    Text = i.CategoryName,
                    Value = i.CategoryId.ToString()
                }),
                CustomPageSelectList = _db.CustomPage.Select(i => new SelectListItem
                {
                    Text = i.CustomName,
                    Value = i.Id.ToString()
                })
            };
            productVM.Product = _db.Product.Find(id);
            if (productVM.Product == null)
            {
                return NotFound();
            }
            
            
            return View(productVM);
        }

        //Post - edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProductViewModel productVM , int id)
        {
            var files = HttpContext.Request.Form.Files;
            string webRootPath = _webHostEnvironment.WebRootPath;
            var objFromDb = _db.Product.AsNoTracking().FirstOrDefault(u => u.ID == id);
            productVM.Product.ID = objFromDb.ID;
            productVM.Product.Image = objFromDb.Image;
            if (files.Count > 0)
            {
                string upload = webRootPath + WebConstants.ImagesPath;
                string fileName = Guid.NewGuid().ToString();
                string extension = Path.GetExtension(files[0].FileName);

                var oldFile = Path.Combine(upload, objFromDb.Image);

                if (System.IO.File.Exists(oldFile))
                {
                    System.IO.File.Delete(oldFile);
                }
                using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                }
                productVM.Product.Image = fileName + extension;
            }
            else
            {

            }
            _db.Product.Update(productVM.Product);

            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        //Get - Delete
        public IActionResult Delete(int id)
        {
            var obj = _db.Product.Find(id);
            if ( id == 0)
            {
                return NotFound();
            }
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        //Post - Delete
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int ? id)
        {
            var obj = _db.Product.Find(id);
            string upload = _webHostEnvironment.WebRootPath + WebConstants.ImagesPath;
            var image = obj.Image;
            var oldFile = Path.Combine(upload, obj.Image);
           
            if (System.IO.File.Exists(oldFile))
            {
                System.IO.File.Delete(oldFile);
            }
            
            _db.Product.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}
