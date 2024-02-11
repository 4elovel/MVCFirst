using MVCFirst.Models.ViewModels;

namespace MVCFirst.Services;

public class CartService : ICartService
{
    public List<CartViewModel> Cart { get; set; } = new List<CartViewModel>();
}
