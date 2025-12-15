using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Storage.Data;
using Storage.Models;
using Storage.Models.ViewModels;

namespace Storage.Controllers
{
    public class ProductsController : Controller
    {
        private readonly StorageContext _context;
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(
            StorageContext context, 
            IProductRepository productRepository,
            ICategoryRepository categoryRepository,
            ILogger<ProductsController> logger)
        {
            _context = context;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _logger = logger;
        }

        // GET: Products?filter=1&filter=2
        public async Task<IActionResult> Index([FromQuery] IEnumerable<int>? filter)
        {
            var filteredProductsList = _productRepository
                .FilterProducts(filter)
                .ToList()
                .Select(p => new ProductDetailsViewModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    OrderDate = p.OrderDate,
                    CategoryId = p.CategoryId,
                    Category = _categoryRepository.AllCategories.First(c => c.Id == p.CategoryId),
                    Shelf = p.Shelf,
                    Count = p.Count,
                    Description = p.Description
                });

            AllProductsViewModel viewModel = new()
            {
                Products = filteredProductsList,
                Count = filteredProductsList.Count(),
                Categories = GetCategorySelects(_categoryRepository.AllCategories, filter),
                SelectedCategoryIds = filter
            };
            return View(viewModel);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _productRepository.GetProductById(id);

            if (product == null)
            {
                return NotFound();
            }

            ProductDetailsViewModel viewModel = new()
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                OrderDate = product.OrderDate,
                CategoryId = product.CategoryId,
                Category = _categoryRepository.AllCategories.First(c => c.Id == product.CategoryId),
                Shelf = product.Shelf,
                Count = product.Count,
                Description = product.Description
            };

            return View(viewModel);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ProductCreateViewModel viewModel = new()
            {
                Categories = GetCategorySelects(_categoryRepository.AllCategories)
            };
            return View(viewModel);
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Price,OrderDate,CategoryId,Shelf,Count,Description")] ProductCreateDto product)
        {
            if (product == null)
            {
                return NotFound(); 
            }

            if (ModelState.IsValid)
            {
                _productRepository.Create(product);
                return RedirectToAction(nameof(Index));
            }

            ProductCreateViewModel viewModel = new()
            {
                Name = product.Name,
                Price = product.Price,
                OrderDate = product.OrderDate,
                CategoryId = product.CategoryId,
                Shelf = product.Shelf,
                Count = product.Count,
                Categories = GetCategorySelects(_categoryRepository.AllCategories, [product.CategoryId])
            };

            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _productRepository.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            var categories = _categoryRepository.AllCategories;
            ProductEditViewModel viewModel = new()
            {
                Product = product,
                Categories = GetCategorySelects(categories, [product.CategoryId]),
            };

            return View(viewModel);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,OrderDate,CategoryId,Shelf,Count,Description")] Product product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _productRepository.Update(id, product);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
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

            var categories = _categoryRepository.AllCategories;
            ProductEditViewModel viewModel = new()
            {
                Product = product,
                Categories = GetCategorySelects(categories, [product.CategoryId])
            };

            return View(viewModel);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Product.FindAsync(id);
            if (product != null)
            {
                _context.Product.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        

        // GET: Products/Summary
        public async Task<IActionResult> Summary()
        {
            return View(_productRepository.GetSummary());
        }

        private bool ProductExists(int id)
        {
            return _productRepository.AllProducts.Any(e => e.Id == id);
        }

        private static List<SelectListItem> GetCategorySelects(IEnumerable<Category> categories, IEnumerable<int>? selectedIds = null)
        {
            return categories.Select(c => new SelectListItem()
            {
                Value = c.Id.ToString(),
                Text = c.Name,
                Selected = selectedIds?.Contains(c.Id) ?? false
            }).ToList();
        }
    }
}
