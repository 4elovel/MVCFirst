namespace MVCFirst.Models.ViewModels;

public class CartViewModel
{
    public CartViewModel(Product product)
    {
        this.Id = product.Id;
        this.Name = product.Name;
        this.Description = product.Description;
        this.Price = product.Price;
        Count = 1;
    }
    public CartViewModel(Product product, int count)
    {
        this.Id = product.Id;
        this.Name = product.Name;
        this.Description = product.Description;
        this.Price = product.Price;
        Count = count;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Price { get; set; }
    public int Count { get; set; }
}
