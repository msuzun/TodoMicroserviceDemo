using FirstMicroservice.Categories.WebAPI.Context;
using FirstMicroservice.Categories.WebAPI.Dtos;
using FirstMicroservice.Categories.WebAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});
var app = builder.Build();

app.MapGet("categories/getall", async (ApplicationDbContext context, CancellationToken cancellationToken) =>
{
    var categories = await context.Categories.ToListAsync(cancellationToken);
    return categories;
});

app.MapPost("categories/create", async (CreateCategoryDto request, ApplicationDbContext context, CancellationToken cancelationToken) =>
{
    bool isNameExists = await context.Categories.AnyAsync(p => p.Name == request.name, cancelationToken);
    if (isNameExists)
    {
        return Results.BadRequest(new { Message = "Category already exists" });
    }
    Category category = new()
    {
        Name = request.name,
    };
    await context.Categories.AddAsync(category, cancelationToken);
    await context.SaveChangesAsync(cancelationToken);

    return Results.Ok( new { Message =  "Category create is successfully" });
});

using(var scoped = app.Services.CreateScope())
{
    var srv = scoped.ServiceProvider;
    var context = srv.GetRequiredService<ApplicationDbContext>();
    context.Database.Migrate();
}

app.Run();
