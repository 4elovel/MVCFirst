using MVCFirst.Models.ViewModels;

namespace MVCFirst.Services;

public interface ICartService
{
    public List<CartViewModel> Cart { get; set; }
}
