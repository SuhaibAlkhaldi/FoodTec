using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using RestaurantSys.Interface;
using RestaurantSys.Models;
using RestaurantSys.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<FoodDeliveryManagementSystemDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ICategory , CategoryService>();
builder.Services.AddScoped<IOffer, OfferService>();
builder.Services.AddScoped<IItem, ItemService>();
builder.Services.AddScoped<INotification, NotificationService>();
builder.Services.AddScoped<IFavorite, FavoriteService>();
builder.Services.AddScoped<IOrderItem, OrderItemService>();
builder.Services.AddScoped<IOrder, OrderService>();
builder.Services.AddScoped<IPaymentMethod, PaymentMethodService>();
builder.Services.AddScoped<IAddress, AddressService>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});



var app = builder.Build();

app.UseCors("AllowAllOrigins");
// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();
//app.UseSwaggerUI(c =>
//{
//    c.SwaggerEndpoint("/swagger/v1/swagger.json", "RestaurantSys");
//    c.RoutePrefix = string.Empty;
//});

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Uploads")),
    RequestPath = "/uploads"
});
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
