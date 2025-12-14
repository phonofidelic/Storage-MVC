using System;

namespace Storage.Models;

public class MockDb
{
    public IEnumerable<Category> Categories { get; set;} = [];
    public IEnumerable<Product> Products { get; set; } = [];
}
