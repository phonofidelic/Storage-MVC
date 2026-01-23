using System.ComponentModel.DataAnnotations;

namespace Storage.Models.ViewModels
{
    public class ProductSummaryViewModel
    {
        public ProductSummary ProductSummary { get; set; } = default!;

        public IEnumerable<ProductSummary> ProductSummaries { get; set;  } = [];

        [DataType(DataType.Currency)]
        public int TotalInventoryValue { get; set; }
    }

    public class ProductSummary
    {
        public string Name { get; set; } = string.Empty;

        [DataType(DataType.Currency)]
        public int Price { get; set; }

        public int Count { get; set; }
        [DataType(DataType.Currency)]

        public int InventoryValue { get; set; }
    }
}