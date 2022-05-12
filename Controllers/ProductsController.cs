using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project.Models;
using Project.Repositories;
using Project.ViewModels;

namespace Project.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductRepo productRepo;
        private readonly ICategoryRepository categoryRepository;
        private readonly IImageRepo imageRepo;

        public ProductsController(IProductRepo _ProductRepo, ICategoryRepository _categoryRepository, IImageRepo imageRepo)
        {
            productRepo = _ProductRepo;
            categoryRepository = _categoryRepository;
            this.imageRepo = imageRepo;
        }

        // GET: Products
        public IActionResult Index()
        {
            ProductCategoryVM vm = new ProductCategoryVM()
            {
                Categories = categoryRepository.GetAllWithProducts(),
                Products=productRepo.GetAll(),
             
            };

            return View(vm);
        }
        public IActionResult List()
        {
            return View(productRepo.GetAll());
        }

        // GET: Products/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = productRepo.GetByIdwithCategory(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["Categories"] = categoryRepository.GetAll();
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Product product)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + product.imgFile.FileName;
                bool result = await imageRepo.StoreImage("product", uniqueFileName, product.imgFile);
                if (result)
                {
                    product.Img = uniqueFileName;
                    try
                    {
                        productRepo.Insert(product);
                        return RedirectToAction(nameof(Index));
                    }
                    catch (Exception ex)
                    {
                        ViewData["Categories"] = categoryRepository.GetAll();
                        return View(product);
                    }
                }                
            }
            ViewData["Categories"] = categoryRepository.GetAll();
            return View(product);
        }

        // GET: Products/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = productRepo.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryID"] = new SelectList(categoryRepository.GetAll(), "ID", "Name");
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id,  Product product)
        {
            if (id != product.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (product.imgFile != null)
                    {
                        if (product.Img != null) imageRepo.DeleteImage(product.Img);
                        string uniqueFileName = Guid.NewGuid().ToString() + "_" + product.imgFile.FileName;
                        imageRepo.StoreImage("product", uniqueFileName, product.imgFile);
                        product.Img = uniqueFileName;
                    }
                    productRepo.Edit(product);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ID))
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
            ViewData["CategoryID"] = new SelectList(categoryRepository.GetAll(), "ID", "Name");
            return View(product);
        }

        // GET: Products/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product =  productRepo.GetById(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            imageRepo.DeleteImage(productRepo.GetById(id).Img);
            productRepo.DeleteById(id);
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return productRepo.GetAll().Any(p => p.ID == id);
        }
    }
}
