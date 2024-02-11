using Microsoft.EntityFrameworkCore;

namespace MVCFirst.Models;
[Index(nameof(Name), IsUnique = true)]
public class Category
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Slug { get; set; }

}
