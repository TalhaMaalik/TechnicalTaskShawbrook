using BusinessLayer.Processor;
using BusinessLayer.Processors.Factory;
using BusinessLayer.Processors.Visitor;
using BusinessLayer.Validator;
using DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(opt =>opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerConnection")));
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<IValidator, RequestValidator>();
builder.Services.AddScoped<IItemFactory, ItemFactory>();
builder.Services.AddScoped<IPurchaseOrderProcessor, PurchaseOrderProcessor>();
builder.Services.AddTransient<IItemVisitor, ItemVisitor>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();
app.UseAuthorization();

app.MapControllers();

app.Run();
