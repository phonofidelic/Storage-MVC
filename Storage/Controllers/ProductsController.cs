using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Storage.Models;
using Storage.Models.Entities;
using Storage.Models.ViewModels;
using Storage.Services;

namespace Storage.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductService _productService;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICategoryService _categoryService;
        private readonly IImageRepository _imageRepository;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(
            IProductRepository productRepository,
            IProductService productService,
            ICategoryRepository categoryRepository,
            ICategoryService categoryService,
            IImageRepository imageRepository,
            ILogger<ProductsController> logger)
        {
            _productRepository = productRepository;
            _productService = productService;
            _categoryRepository = categoryRepository;
            _categoryService = categoryService;
            _imageRepository = imageRepository;
            _logger = logger;
        }

        // GET: Products?filter=1&filter=2
        public async Task<IActionResult> Index([FromQuery] IEnumerable<int>? filter)
        {
            var allCategories = await _categoryRepository.GetAllCategoriesAsync();
            var filteredProducts = await _productRepository.FilterProductsAsync(filter);
            
            var filteredProductsList = filteredProducts
                .ToList()
                .Select(_productService.MapProductDetails);

            AllProductsViewModel viewModel = new()
            {
                Products = filteredProductsList,
                Count = filteredProductsList.Count(),
                Categories = _categoryService.GetCategorySelects(allCategories, filter),
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

            var product = await _productRepository.GetProductByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            var allCategories = await _categoryRepository.GetAllCategoriesAsync();

            ProductDetailsViewModel viewModel = _productService.MapProductDetails(product);

            return View(viewModel);
        }

        // GET: Products/Create
        public async Task<IActionResult> Create()
        {
            var allCategories = await _categoryRepository.GetAllCategoriesAsync();
            ProductCreateViewModel viewModel = new()
            {
                Categories = _categoryService.GetCategorySelects(allCategories)
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
                await _productRepository.CreateAsync(product);
                return RedirectToAction(nameof(Index));
            }

            var allCategories = await _categoryRepository.GetAllCategoriesAsync();
            ProductCreateViewModel viewModel = new()
            {
                Name = product.Name,
                Price = product.Price,
                OrderDate = product.OrderDate,
                CategoryId = product.CategoryId,
                Shelf = product.Shelf,
                Count = product.Count,
                Categories = _categoryService.GetCategorySelects(allCategories, [product.CategoryId])
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

            var product = await _productRepository.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ProductDetailsViewModel productViewModel = _productService.MapProductDetails(product);

            var categories = await _categoryRepository.GetAllCategoriesAsync();
            ProductEditViewModel viewModel = new()
            {
                Product = _productService.MapProductDetails(product),
                Categories = _categoryService.GetCategorySelects(categories, [product.CategoryId]),
            };

            return View(viewModel);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,OrderDate,CategoryId,Shelf,Count,Description,Image")] ProductDetailsViewModel viewModel)
        {
            var product = await _productRepository.GetProductByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _productRepository.UpdateAsync(id, viewModel);
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

            var categories = await _categoryRepository.GetAllCategoriesAsync();
            //ProductEditViewModel viewModel = new()
            //{
            //    Product = product,
            //    Categories = _categoryService.GetCategorySelects(categories, [product.CategoryId])
            //};

            return View(viewModel);
        }

        // POST: Products/SaveProductImage/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>SaveProductImage(int id, [Bind("ImagePath,AltText")] CreateImageDto createImageDto)
        {
            var product = await _productRepository.GetProductByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            var productDetailsViewModel = _productService.MapProductDetails(product, createImageDto);

            if (ModelState.IsValid)
            {
                try
                {
                    await _imageRepository.CreateAsync(createImageDto);
                    await _productRepository.UpdateAsync(id, productDetailsViewModel);
                } catch (DbUpdateConcurrencyException) {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return View(nameof(Edit), productDetailsViewModel);
            }

            return View(nameof(Edit), _productService.MapProductDetails(product));
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productRepository.GetProductByIdAsync(id);
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
            _productRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        // GET: Products/Summary
        public async Task<IActionResult> Summary()
        {
            var allProducts = await _productRepository.FilterProductsAsync([]);
            var productSummaries = allProducts.Select(p => _productService.GetProductSummary(p));

            ProductSummaryViewModel viewModel = new()
            {
                ProductSummaries = productSummaries,
                TotalInventoryValue = _productService.GetTotalInventoryValue(productSummaries)
            };

            return View(viewModel);
        }

        public async Task<IActionResult> AddProductImage(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productRepository.GetProductByIdAsync(id);


            if (product == null)
            {
                return NotFound();
            }
            
            var allCategories = await _categoryRepository.GetAllCategoriesAsync();

            ProductEditViewModel viewModel = new()
            {
                Product = _productService.MapProductDetails(product),

            };

            return View(nameof(Edit), viewModel);
        }

        private bool ProductExists(int id)
        {
            return _productRepository.AllProducts.Any(e => e.Id == id);
        }
    }
}
