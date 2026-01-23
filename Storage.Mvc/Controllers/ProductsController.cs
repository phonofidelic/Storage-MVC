using System.Text.RegularExpressions;
using System.Web;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Storage.Core.Entities;
using Storage.Models;
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
        private readonly ILogger<ProductsController> _logger;
        
        // ToDo: Set _maxTake in config?
        private const int _maxTake = 50;

        public ProductsController(
            IProductRepository productRepository,
            IProductService productService,
            ICategoryRepository categoryRepository,
            ICategoryService categoryService,
            ILogger<ProductsController> logger)
        {
            _productRepository = productRepository;
            _productService = productService;
            _categoryRepository = categoryRepository;
            _categoryService = categoryService;
            _logger = logger;
        }

        // GET: Products?filter=1&filter=2
        public async Task<IActionResult> Index(
            [Bind("ProductListParameters")] ProductIndexViewModel inViewModel,
            [FromQuery] int? removeCategory,
            [FromQuery] decimal? minPrice,
            [FromQuery] decimal? maxPrice,
            [FromQuery] DateTime? minOrderDate,
            [FromQuery] DateTime? maxOrderDate,
            [FromQuery] int? limit = _maxTake,
            [FromQuery] int? page = 1,
            [FromQuery] ProductSortBy sort = ProductSortBy.Name,
            [FromQuery] SortOrder order = SortOrder.Ascending
            )
        {
            int currentPage = page ?? inViewModel.ListParameters.CurrentPage;
            int pageLimit = limit ?? inViewModel.ListParameters.PageLimit;
            int offset = pageLimit * (currentPage - 1);

            // ToDo: Move to model binder?
            string categoryMatcher = @"categories=\d+";
            IEnumerable<int> selectedCategoryIds = Regex.Matches(HttpUtility.UrlDecode(Request.GetDisplayUrl()), categoryMatcher)
                .Select(m => m.Value.Replace("categories=", ""))
                .Select(c => {
                    if (int.TryParse(c, out int catId))
                    {
                        return catId;
                    }
                    return 0;   
                })
                .Distinct()
                .Where(c => c != removeCategory)
                .ToList() ?? [];

            decimal defaultMinPrice = await _productRepository.GetMinPrice();
            decimal defaultMaxPrice = await _productRepository.GetMaxPrice();
            decimal minPriceOrDefault = minPrice ?? defaultMinPrice;
            decimal maxPriceOrDefault = maxPrice ?? defaultMaxPrice;
            
            IEnumerable<Product> filteredProducts = 
                await _productRepository.FilterProductsAsync(
                    minPriceOrDefault, 
                    maxPriceOrDefault, 
                    selectedCategoryIds,
                    minOrderDate, 
                    maxOrderDate);

            switch(sort)
            {
                case ProductSortBy.Name:
                    filteredProducts = order == SortOrder.Ascending ? 
                        filteredProducts.OrderBy(p => p.Name) : 
                        filteredProducts.OrderByDescending(p => p.Name);
                    break;

                case ProductSortBy.Price:
                    filteredProducts = order == SortOrder.Ascending ? 
                        filteredProducts.OrderBy(p => p.Price) :
                        filteredProducts.OrderByDescending(p => p.Price);
                    break;

                case ProductSortBy.OrderDate:
                    filteredProducts = order == SortOrder.Ascending ? 
                        filteredProducts.OrderBy(p => p.OrderDate) :
                        filteredProducts.OrderByDescending(p => p.OrderDate);
                    break;

                case ProductSortBy.Category:
                    filteredProducts = order == SortOrder.Ascending ? 
                        filteredProducts.OrderBy(p => p.Category.Name) :
                        filteredProducts.OrderByDescending(p => p.Category.Name);
                    break;

                case ProductSortBy.Shelf:
                    filteredProducts = order == SortOrder.Ascending ? 
                        filteredProducts.OrderBy(p => p.Shelf) :
                        filteredProducts.OrderByDescending(p => p.Shelf);
                    break;

                case ProductSortBy.Count:
                    filteredProducts = order == SortOrder.Ascending ? 
                        filteredProducts.OrderBy(p => p.InventoryCount) :
                        filteredProducts.OrderByDescending(p => p.InventoryCount);
                    break;

                default:
                filteredProducts = filteredProducts.OrderBy(p => p.Name);
                    break;
            }

            // Make sure we don't take more than max
            if (pageLimit > _maxTake) pageLimit = _maxTake;

            IEnumerable<ProductListItemViewModel> productListItems = filteredProducts
                .Skip(offset)
                .Take(pageLimit)
                .Select(_productService.MapProductListItem)
                .ToList();

            int totalProductsCount = _productRepository.AllProductsCount;
            int filteredProductsCount = filteredProducts.Count();

            ProductListParameters productListParams = new ()
            {
                MinPrice = (int)minPriceOrDefault,
                MaxPrice = (int)maxPriceOrDefault,
                MinOrderDate = minOrderDate,
                MaxOrderDate = maxOrderDate,
                SelectedCategoryIds = String.Join("&categories=", selectedCategoryIds),
                SortBy = sort,
                SortOrder = order,
                PageLimit = pageLimit,
                Offset = offset,
                CurrentPage = currentPage
            };


            ProductIndexViewModel outViewModel = new()
            {
                Products = productListItems,
                TotalProductsCount = totalProductsCount,
                FilteredProductsCount = filteredProductsCount,
                CategorySelectItems = _categoryService.GetCategorySelects(await _categoryRepository.GetAllCategoriesAsync(), selectedCategoryIds),
                SelectedCategories = await _categoryRepository.GetCategoriesByIdAsync(selectedCategoryIds),
                DefaultMinPrice = (int)defaultMinPrice,
                DefaultMaxPrice = (int)defaultMaxPrice,
                TotalPages = (int)Math.Ceiling(filteredProductsCount / (decimal)pageLimit),
                ListParameters = productListParams
            };

            return View(outViewModel);
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

            // var allCategories = await _categoryRepository.GetAllCategoriesAsync();

            ProductDetailsViewModel viewModel = _productService.MapProductDetails(product);

            return View(viewModel);
        }

        // GET: Products/Create
        public async Task<IActionResult> Create()
        {
            var allCategories = await _categoryRepository.GetAllCategoriesAsync();
            ProductCreateViewModel viewModel = new()
            {
                CategorySelectItems = _categoryService.GetCategorySelects(allCategories)
            };
            return View(viewModel);
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Price,PurchasePrice,OrderDate,CategoryId,Shelf,Count,Description")] ProductCreateViewModel product)
        {
            if (product == null)
            {
                return NotFound(); 
            }

            if (ModelState.IsValid)
            {
                ProductCreateDto productCreate = new ProductCreateDto(
                    Name: product.Name,
                    Price: product.Price,
                    PurchasePrice: product.PurchasePrice,
                    OrderDate: product.OrderDate,
                    CategoryId: product.CategoryId,
                    Shelf: product.Shelf,
                    Count: product.Count,
                    Description: product.Description);

                await _productRepository.CreateAsync(productCreate);
                return RedirectToAction(nameof(Index));
            }

            var allCategories = await _categoryRepository.GetAllCategoriesAsync();
            ProductCreateViewModel viewModel = new()
            {
                Name = product.Name,
                Price = product.Price,
                PurchasePrice = product.PurchasePrice,
                OrderDate = product.OrderDate,
                CategoryId = product.CategoryId,
                Shelf = product.Shelf,
                Count = product.Count,
                CategorySelectItems = _categoryService.GetCategorySelects(allCategories, product.CategoryId),
                Description = product.Description
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

            var categories = await _categoryRepository.GetAllCategoriesAsync();
            ProductEditViewModel viewModel = _productService.MapProductEditViewModel(
                product, 
                _categoryService.GetCategorySelects(categories, product.CategoryId));

            return View(viewModel);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id,Name,Price,OrderDate,Category,CategoryId,Shelf,Count,Description,Image")] ProductEditViewModel productEditViewModel)
        {
            var product = await _productRepository.GetProductByIdAsync(productEditViewModel.Id);

            if (product == null)
            {
                return NotFound();
            }

            var categories = await _categoryRepository.GetAllCategoriesAsync();
            ProductEditViewModel viewModel = _productService.MapProductEditViewModel(
                product, _categoryService.GetCategorySelects(categories, productEditViewModel.CategoryId));

            ImageInputViewModel? image = null;
            
            if (productEditViewModel.Image?.Path != null && productEditViewModel.Image?.Alt == null)
            {
                ModelState.AddModelError("ImageAlt", "An alternative text is required for all images");
            }

            if (productEditViewModel.Image?.Path != null && productEditViewModel.Image?.Alt != null)
            {
                image = new()
                {
                    Path = productEditViewModel.Image.Path,
                    Alt = productEditViewModel.Image.Alt
                };
            }

            if (ModelState.IsValid)
            {
                try
                {
                    ProductEditDto productEditDto = new ProductEditDto(
                        Id: productEditViewModel.Id,
                        Name: productEditViewModel.Name,
                        Price: productEditViewModel.Price,
                        PurchasePrice: productEditViewModel.PurchasePrice,
                        OrderDate: productEditViewModel.OrderDate,
                        CategoryId: productEditViewModel.CategoryId,
                        Shelf: productEditViewModel.Shelf,
                        InventoryCount: productEditViewModel.Count,
                        Description: productEditViewModel.Description
                        // image
                    );
                    await _productRepository.UpdateAsync(productEditDto);
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

            

            return View(viewModel);
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
            var allProducts = _productRepository.AllProducts;
            var productSummaries = allProducts.Select(_productService.GetProductSummary);

            ProductSummaryViewModel viewModel = new()
            {
                ProductSummaries = productSummaries,
                TotalInventoryValue = _productService.GetTotalInventoryValue(productSummaries)
            };

            return View(viewModel);
        }

        private bool ProductExists(int id)
        {
            return _productRepository.AllProducts.Any(e => e.Id == id);
        }
    }
}
