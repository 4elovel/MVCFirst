using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCFirst.Models;
using MVCFirst.Models.ViewModels;

namespace MVCFirst.Controllers;


public class ProductsController : Controller
{
    private readonly ApplicationContext _context;

    public ProductsController(ApplicationContext context)
    {
        _context = context;
    }

    // GET: Products
    public async Task<IActionResult> Index()
    {
        List<ProductTableCardViewModel> ls = new();
        foreach (var i in await _context.Products.Include(c => c.Category).ToListAsync())
        {
            ls.Add(new ProductTableCardViewModel(i));
        }
        return View(ls);
    }

    // GET: Products/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var product = await _context.Products
            .Include(p => p.Category)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (product == null)
        {
            return NotFound();
        }
        return View(product);
    }

    // GET: Products/Create
    public IActionResult Create()
    {
        CreateDTO dto = new(new Product(), _context.Categories.ToList());
        return View(dto);
    }

    // POST: Products/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,Description,Slug,Price,StockQuantity")] Product product, [Bind("Category")] string category, [FromForm] IFormFile? file)
    {


        if (ModelState.IsValid)
        {
            if (file != null)
            {
                var guid = Guid.NewGuid();
                var filepath = Path.Combine("wwwroot", "images", $"{guid}.jpg");
                using (var stream = new FileStream(filepath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                product.Image = Path.Combine("images", $"{guid}.jpg");
                product.Image = product.Image.Insert(0, "\\");
            }
            foreach (var i in _context.Categories.ToList())
            {
                if (i.Name == category)
                {
                    product.Category = i;
                }
            }
            _context.Add(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(product);
    }

    // GET: Products/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var product = await _context.Products.Include(p => p.Category).Where(p => p.Id == id).FirstOrDefaultAsync();
        if (product == null)
        {
            return NotFound();
        }
        CreateDTO dto = new(product, _context.Categories.ToList());
        return View(dto);
    }

    // POST: Products/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Slug,Price,StockQuantity")] Product product, [Bind("Category")] string category, [FromForm] IFormFile? file)
    {
        if (id != product.Id)
        {
            return NotFound();
        }
        if (ModelState.IsValid)
        {
            try
            {
                var oldProduct = _context.Products.Where(p => p.Id == product.Id).First();
                foreach (var i in _context.Categories.ToList())
                {
                    if (i.Name == category)
                    {
                        product.Category = i;
                    }
                }
                if (file != null)
                {
                    var guid = Guid.NewGuid();
                    var filepath = Path.Combine("wwwroot", "images", $"{guid}.jpg");
                    using (var stream = new FileStream(filepath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    product.Image = Path.Combine("images", $"{guid}.jpg");
                    product.Image = product.Image.Insert(0, "\\");
                }
                else
                {
                    product.Image = oldProduct.Image;
                }
                //oldProduct = product;
                //_context.Products.Update(product);
                oldProduct.Name = product.Name;
                oldProduct.Description = product.Description;
                oldProduct.Category = product.Category;
                oldProduct.Slug = product.Slug;
                oldProduct.Price = product.Price;
                oldProduct.StockQuantity = product.StockQuantity;
                oldProduct.Image = product.Image;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(product.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(product);
    }

    // GET: Products/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var product = await _context.Products
            .FirstOrDefaultAsync(m => m.Id == id);
        if (product == null)
        {
            return NotFound();
        }

        return View(product);
    }

    // POST: Products/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product != null)
        {
            _context.Products.Remove(product);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ProductExists(int id)
    {
        return _context.Products.Any(e => e.Id == id);
    }


}
