namespace MVCFirst.Models.ViewModels;

public class ProductTableCardViewModel
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? CategoryName { get; set; }
    public string? Description { get; set; }
    public string? Price { get; set; }
    public ProductTableCardViewModel(Product product)
    {
        Id = product.Id;
        Name = product.Name;
        CategoryName = product.Category.Name;
        Description = product.Description;
        Price = product.Price;
    }
}
