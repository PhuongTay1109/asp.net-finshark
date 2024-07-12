using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using MinimalImageUploadAPI.Data;
using MinimalImageUploadAPI.Services;

var  AllowSpecificOrigins = "_AllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JsonOptions>(opts => opts.SerializerOptions.IncludeFields = true);


// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQLConnection")));

builder.Services.AddSingleton<IImageService, ImageService>(); 

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AllowSpecificOrigins,
                          policy =>
                          {
                              policy.WithOrigins("http://127.0.0.1:5500")
                                                  .AllowAnyHeader()
                                                  .AllowAnyMethod()
                                                  .AllowAnyOrigin();
                          });
});


var app = builder.Build();

app.UseHttpsRedirection();
app.UseCors(AllowSpecificOrigins);

app.MapPost("/upload", async Task<IResult> (IImageService imageService, IFormFile image, string name) =>
{
    try
    {
        var newImage = await imageService.UploadImageAsync(image, name);
        // return Results.Ok(new { uploadedImage.Id, uploadedImage.Name });
        return Results.Ok(new { Message = "Success uploaded" });
    }
    catch (ArgumentException ex)
    {
        return Results.BadRequest(new { Message = ex.Message });
    }
}).DisableAntiforgery();



app.Run();
