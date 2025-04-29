// Program.cs
using Kursmoment3Project.Data;
using Kursmoment3Project.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Registrera DbContext med SQLite
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite("Data Source=kursmoment3.db"));

var app = builder.Build();

// Automatiskt skapa DB
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.EnsureCreated();
}

// GET: Hämta alla produkter
app.MapGet("/products", async (ApplicationDbContext db) =>
{
    return await db.Products.ToListAsync();
});

// POST: Skapa en produkt
app.MapPost("/products", async (ApplicationDbContext db, Product product) =>
{
    db.Products.Add(product);
    await db.SaveChangesAsync();
    return Results.Created($"/products/{product.Id}", product);
});

// PUT: Uppdatera en produkt
app.MapPut("/products/{id}", async (ApplicationDbContext db, int id, Product updatedProduct) =>
{
    var product = await db.Products.FindAsync(id);
    if (product == null) return Results.NotFound();

    product.Name = updatedProduct.Name;
    product.Price = updatedProduct.Price;
    await db.SaveChangesAsync();

    return Results.Ok(product);
});

// DELETE: Ta bort en produkt
app.MapDelete("/products/{id}", async (ApplicationDbContext db, int id) =>
{
    var product = await db.Products.FindAsync(id);
    if (product == null) return Results.NotFound();

    db.Products.Remove(product);
    await db.SaveChangesAsync();

    return Results.Ok();
});

app.Run();
