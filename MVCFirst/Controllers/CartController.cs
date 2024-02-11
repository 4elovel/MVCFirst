using Microsoft.AspNetCore.Mvc;
using MVCFirst.Models;
using MVCFirst.Services;

namespace MVCFirst.Controllers;

public class CartController : Controller
{
    private readonly ApplicationContext _context;
    public ICartService cartService;

    public CartController(ApplicationContext context, ICartService cartService)
    {
        _context = context;
        this.cartService = cartService;
    }
    public async Task<IActionResult> Add(int id)
    {
        bool leaver = true;
        var el = _context.Products.Where(p => p.Id == id).FirstOrDefault();
        foreach (var i in cartService.Cart)
        {
            if (i.Id == id)
            {
                i.Count++;
                leaver = false;
                break;
            }
        }
        if (leaver)
        {
            cartService.Cart.Add(new Models.ViewModels.CartViewModel(el));
        }
        //return View("\\Views\\Products\\Index.cshtml");
        return RedirectToAction("Index", "Products");
    }
    public async Task<IActionResult> Index()
    {
        for (int i = 0; i < cartService.Cart.Count; i++)
        {

            var buf = _context.Products.Where(p => p.Id == cartService.Cart[i].Id).FirstOrDefault();
            if (buf != default && cartService.Cart[i].Count > 0)
            {
                cartService.Cart[i] = new Models.ViewModels.CartViewModel(buf, cartService.Cart[i].Count);
                continue;
            }
            cartService.Cart.RemoveAt(i);

        }
        return View(cartService.Cart);
    }
    public async Task<IActionResult> Delete(int id)
    {
        foreach (var i in cartService.Cart)
        {
            if (i.Id == id)
            {
                i.Count--;
            }
        }
        return RedirectToAction("Index");
    }
    public async Task<IActionResult> DeleteAll(int id)
    {
        for (int i = 0; i < cartService.Cart.Count; i++)
        {
            if (cartService.Cart[i].Id == id)
            {
                cartService.Cart.RemoveAt(i);
                break;
            }
        }
        return RedirectToAction("Index");
    }
}
