using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Models;
using Project.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly IImageRepo imageRepo;
        private readonly ICategoryRepository categoryRepository;

        public CategoriesController(IImageRepo imageRepo, ICategoryRepository _categoryRepository)
        {
            this.imageRepo = imageRepo;
            categoryRepository = _categoryRepository;
        }

        // GET: Categories
        public IActionResult Index()
        {
            return View(categoryRepository.GetAll());
        }



        //ADMIN ROLE
        // GET: Categories
        [Authorize(Roles = "Admin")]
        public IActionResult List()
        {
            return View(categoryRepository.GetAll());
        }

        // GET: Categories/Details/5
        [Authorize(Roles = "Customer")]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var category = categoryRepository.GetById(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult AdminDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var category = categoryRepository.GetById(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Categories/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + category.imgFile.FileName;
                bool result = await imageRepo.StoreImage("category",uniqueFileName, category.imgFile);
                if (result)
                {
                    category.Img = uniqueFileName;
                    try
                    {
                        categoryRepository.CreateCagtegory(category);
                        return RedirectToAction(nameof(Index));
                    }
                    catch (Exception ex)
                    {
                        return View(category);
                    }
                }
            }
            return View(category);
        }

        // GET: Categories/Edit/5
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = categoryRepository.GetById(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, Category category)
        {
            if (id != category.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (category.imgFile != null) 
                    {
                        if(category.Img != null) imageRepo.DeleteImage(category.Img);
                        string uniqueFileName = Guid.NewGuid().ToString() + "_" + category.imgFile.FileName;
                        imageRepo.StoreImage("category",uniqueFileName, category.imgFile);
                        category.Img = uniqueFileName;
                    }
                    categoryRepository.UpdateCagtegory(id, category);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Categories/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = categoryRepository.GetById(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteConfirmed(int id)
        {
            imageRepo.DeleteImage(categoryRepository.GetById(id).Img);
            categoryRepository.DeleteCagtegory(id);
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            return categoryRepository.GetAll().Any(e => e.ID == id);
        }
    }
}
