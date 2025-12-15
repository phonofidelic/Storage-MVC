using Storage.Models.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace Storage.Models.ViewModels;

public class ProductDetailsViewModel : Product
{   
        public Category Category { get; set;} = default!;
}
