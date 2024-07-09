using Microsoft.EntityFrameworkCore;
using MinimalImageUploadAPI.Data;
using MinimalImageUploadAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQLConnection")));

var app = builder.Build();

// Migrate the database during startup. Must be synchronous.
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.MapPost("/upload", async (AppDbContext context, IFormFile image, string name) =>
{
    if (image == null || image.Length == 0)
        return Results.BadRequest("No image provided.");

    using var memoryStream = new MemoryStream();
    await image.CopyToAsync(memoryStream);
    var imageData = memoryStream.ToArray();

    Console.WriteLine(name);

    var newImage = new ImageModel
    {
        Name = name,
        ImageData = imageData
    };

    context.Images.Add(newImage);
    await context.SaveChangesAsync();

    return Results.Ok(new { Id = newImage.Id, Name = newImage.Name });
}).DisableAntiforgery();

app.Run();
