using Microsoft.EntityFrameworkCore;
using MVCFirst.Models;
using MVCFirst.Services;

namespace MVCFirst;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        string connection = builder.Configuration.GetConnectionString("DefaultConnection")!;
        builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlite(connection));
        builder.Services.AddSingleton<ICartService, CartService>();

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        var app = builder.Build();



        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        /*        using (ApplicationContext db = new())
                {
                    db.Categories.Add(new Category { Name = "new", Slug = "/new" });
                    db.SaveChanges();
                    db.Products.Add(new Product { Name = "SimpleProduct", Description = "Description of simple product", Category = db.Categories.First() });
                    db.SaveChanges();
                }*/

        app.Run();
    }
}
