using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.AspNetCore.Mvc.Rendering;
using Storage.Core.Entities;

namespace Storage.Models.ViewModels
{
    public class ProductIndexViewModel
    {
        public ProductListItemViewModel Product { get; set; } = default!;

        public int TotalProductsCount { get; set; }

        public int FilteredProductsCount { get; set; }

        public IEnumerable<ProductListItemViewModel> Products { get; set; } = [];
        
        [Display(Name = "category")]
        public IEnumerable<Category> SelectedCategories { get; set; } = [];

        public List<SelectListItem> CategorySelectItems { get; set; } = [];

        public string CultureName {get; set; } = CultureInfo.CurrentCulture.Name;

        public string IsoCurrencySymbol { get; set; } = RegionInfo.CurrentRegion.ISOCurrencySymbol;

        public int DefaultMinPrice { get; set; }
        
        public int DefaultMaxPrice { get; set; }

        public int TotalPages { get; set; }

        // Product list sort/filter/pagination parameters
        public ProductListParameters ListParameters { get; set; } = default!;
    }

    [ModelBinder(BinderType = typeof(ProductListParametersModelBinder))]
    public class ProductListParameters
    {
        [Display(Name = "max price")]
        [DataType(DataType.Currency)]
        public int MaxPrice { get; set; }

        [Display(Name = "min price")]
        [DataType(DataType.Currency)]
        public int MinPrice { get; set; }

        [Display(Name = "min. order date")]
        [DataType(DataType.Date)]
        public DateTime? MinOrderDate { get; set; }

        [Display(Name = "max. order date")]
        [DataType(DataType.Date)]
        public DateTime? MaxOrderDate { get; set; }

        public string? SelectedCategoryIds { get; set; } = string.Empty;

        public ProductSortBy SortBy { get; set; }

        public SortOrder SortOrder { get; set; }

        public int PageLimit { get; set; } = 50;

        public int Offset { get; set; }

        public int CurrentPage { get; set; } = 1;
    }

    // https://learn.microsoft.com/en-us/answers/questions/1195314/receive-complex-object-fromquery-in-net-6-0-web-ap
    public class ProductListParametersModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            ArgumentNullException.ThrowIfNull(bindingContext);

            // Get values from bindingContext
            var valueProvider = bindingContext.ValueProvider;

            var result = new ProductListParameters();

            var maxPriceValues = valueProvider.GetValue("MaxPrice");
            if (maxPriceValues.Length > 0)
            {
                result.MaxPrice = Convert.ToInt32(maxPriceValues.FirstValue);
            }

            var minPriceValues = valueProvider.GetValue("MinPrice");
            if (minPriceValues.Length > 0)
            {
                result.MinPrice = Convert.ToInt32(minPriceValues.FirstValue);
            }

            var minOrderDateValues = valueProvider.GetValue("MinOrderDate");
            if (minOrderDateValues.Length > 0)
            {
                result.MinOrderDate = Convert.ToDateTime(minOrderDateValues.FirstValue);
            }

            var maxOrderDateValues = valueProvider.GetValue("MaxOrderDate");
            if (maxOrderDateValues.Length > 0)
            {
                result.MaxOrderDate = Convert.ToDateTime(maxOrderDateValues.FirstValue);
            }

            var selectedCategoryIdsValues = valueProvider.GetValue("SelectedCategoryIds");
            if (selectedCategoryIdsValues.Length > 0)
            {
                // result.SelectedCategoryIds = selectedCategoryIdsValues.Select(c => Convert.ToInt32(c));
                result.SelectedCategoryIds = selectedCategoryIdsValues.FirstValue;
            }

            var sortOrderValues = valueProvider.GetValue("SortOrder");
            if (sortOrderValues.Length > 0)
            {
                SortOrder sortOrder;
                var canParseSortOrderValue = Enum.TryParse<SortOrder>(sortOrderValues.FirstValue, true, out sortOrder);

                result.SortOrder = canParseSortOrderValue ? sortOrder : SortOrder.Ascending;
            }

            var sortbyValues = valueProvider.GetValue("SortBy");
            if (sortOrderValues.Length > 0)
            {
                ProductSortBy sortBy;
                var canParseSortByValue = Enum.TryParse<ProductSortBy>(sortbyValues.FirstValue, true, out sortBy);

                result.SortBy = canParseSortByValue ? sortBy : ProductSortBy.Name;
            }

            var pageLimitValues = valueProvider.GetValue(nameof(ProductListParameters.PageLimit));
            if (pageLimitValues.Length > 0)
            {
                result.PageLimit = Convert.ToInt32(pageLimitValues.FirstValue);
            }

            var offsetValues = valueProvider.GetValue(nameof(ProductListParameters.Offset));
            if (offsetValues.Length > 0)
            {
                result.Offset = Convert.ToInt32(offsetValues.FirstValue);
            }

            var currentPageValues = valueProvider.GetValue(nameof(ProductListParameters.CurrentPage));
            if (currentPageValues.Length > 0)
            {
                int currentPageValue = Convert.ToInt32(currentPageValues.FirstValue);
                result.CurrentPage = currentPageValue > 0 ? currentPageValue : 1;
            }

            bindingContext.Result = ModelBindingResult.Success(result);
            return Task.CompletedTask;
        }
    }

    public class ProductListParametersBinderProvider : IModelBinderProvider
    {
        public IModelBinder? GetBinder(ModelBinderProviderContext context)
        {
            ArgumentNullException.ThrowIfNull(context);

            if (context.Metadata.ModelType == typeof(ProductListParameters))
            {
                return new BinderTypeModelBinder(typeof(ProductListParametersModelBinder));
            }

            return null;
        }
    }
}
