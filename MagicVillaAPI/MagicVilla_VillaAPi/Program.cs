
using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Extensions.Hosting;

// khởi tạo
// logger is already registered inside the create builder 
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Cấu hình Logger sử dụng thư viện Serilog và ghi log theo ngày vào file, này là customize log bằng Serilog
/*Log.Logger = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.File("log/villaLogs.txt", rollingInterval: RollingInterval.Day).CreateLogger();
builder.Host.UseSerilog();
*/

builder.Services.AddDbContext<ApplicationDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQLConnection"));
});


builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IVillaService, VillaService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// khởi chạy ứng dụng và bắt đầu lắng nghe các yêu cầu từ clients.
app.Run();

// File Program.cs chính là điểm khởi đầu của ứng dụng ASP.NET Core
// và là nơi tất cả các cấu hình cần thiết được thực hiện trước khi ứng dụng bắt đầu chạy.
//Nó là một phần quan trọng của kiến trúc và cấu trúc của dự án ASP.NET Core.
