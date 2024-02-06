using Application.NET.Data;
using Application.NET.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Application.NET.Controllers
{
	public class CategoryController : Controller
	{
		private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
            public IActionResult Index()
		{
            List<Category> objCategoryList = _db.Categories.ToList();
            return View(objCategoryList);
		}

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString()) {
                ModelState.AddModelError("name", "Name and Display Order cannot be same!");
            }

            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
				return RedirectToAction("Index");
			}
            return View();
		}

		public IActionResult Edit(int? id)
		{
			if (id == null|| id == 0) 
			{
				return NotFound();
			}
			Category categoryFromdb = _db.Categories.Find(id);
			if (categoryFromdb == null)
			{
				return NotFound();
			}
			return View(categoryFromdb);
		}
		[HttpPost]
		public IActionResult Edit(Category obj)
		{
			if (ModelState.IsValid)
			{
				_db.Categories.Update(obj);
				_db.SaveChanges();
				return RedirectToAction("Index");
			}
			return View();
		}

		public IActionResult Delete()
		{
			return View();
		}
	}
}
