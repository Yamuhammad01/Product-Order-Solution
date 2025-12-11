using Microsoft.EntityFrameworkCore;
using ProductOrder.Application.Interfaces;
using ProductOrder.Application.Services;
using ProductOrder.Infrastructure.Persistence;
using ProductOrder.Infrastructure.Repository;


var builder = WebApplication.CreateBuilder(args);


// Configure for Render deployment
var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



// configure postgres connection string
builder.Services.AddDbContext<ProductOrderDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));



builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var app = builder.Build();

// Configure the HTTP request pipeline.


// use swagger UI in production
    app.UseSwagger();
    app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();



app.MapControllers();

app.MapGet("/", () => "ProductOrder API is running");// simple health check endpoint 


app.Run();
