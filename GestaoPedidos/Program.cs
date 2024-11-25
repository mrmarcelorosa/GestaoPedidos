using GestaoPedidos.Data;
using GestaoPedidos.Exceptions;
using GestaoPedidos.Models;
using GestaoPedidos.Repositories.Implementations;
using GestaoPedidos.Repositories.Interfaces;
using GestaoPedidos.Services;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<IRepository<Client>, ClientRepository>();
builder.Services.AddScoped<IRepository<Employee>, EmployeeRepository>();
builder.Services.AddScoped<IRepository<Product>, ProductRepository>();
builder.Services.AddScoped<IRepository<Address>, AddressRepository>();
builder.Services.AddScoped<IRepository<Order>, OrderRepository>();
builder.Services.AddScoped<IRepository<OrderItem>, OrderItemRepository>();

//services
builder.Services.AddScoped<ClientService>();
builder.Services.AddScoped<EmployeeService>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<AddressService>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<OrderItemService>();

//repositories
builder.Services.AddScoped<ClientRepository>();
builder.Services.AddScoped<ProductRepository>();
builder.Services.AddScoped<EmployeeRepository>();
builder.Services.AddScoped<AddressRepository>();
builder.Services.AddScoped<OrderRepository>();
builder.Services.AddScoped<OrderItemRepository>();

builder.Services.AddControllers(options =>
{
    options.Filters.Add(new ValidateModelFilter());
});

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Gestão de Pedidos API",
        Version = "v1",
        Description = "API para gerenciamento de pedidos e clientes."
    });

    options.IncludeXmlComments("obj\\Debug\\net7.0\\GestaoPedidos.xml");
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorApp", policy =>
    {
        policy.WithOrigins("https://localhost:7197")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

/*builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
            });*/


//Serializa
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles; 
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });





var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Habilitar CORS
app.UseCors("AllowBlazorApp");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Use(async (context, next) =>
{
    try
    {
        await next.Invoke();
    }
    catch (ApiExceptions ex)
    {
        context.Response.StatusCode = ex.StatusCode;
        context.Response.ContentType = "application/json";
        var response = new
        {
            error = ex.Message,
            details = ex.Details
        };
        await context.Response.WriteAsJsonAsync(response);
    }
    catch (Exception ex)
    {
        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";
        var response = new
        {
            error = "An unexpected error occurred",
            details = ex.Message
        };
        await context.Response.WriteAsJsonAsync(response);
    }
});


app.Run();
