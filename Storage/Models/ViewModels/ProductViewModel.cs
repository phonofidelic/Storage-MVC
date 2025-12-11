using System.ComponentModel.DataAnnotations;

namespace Storage.Models.ViewModels
{
    public class ProductViewModel
    {
        public string Name { get; set; } = string.Empty;
        [DataType(DataType.Currency)]
        public int Price { get; set; }
        public int Count { get; set; }
        [DataType(DataType.Currency)]
        public int InventoryValue { get; set; }
    }
}