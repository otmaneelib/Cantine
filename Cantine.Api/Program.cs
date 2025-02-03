using Application.Services;
using Core.Dtos;
using Core.Interfaces.Mappings;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Interfaces.Validation;
using Core.Mappings;
using Core.Validations;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ClientDTOValidator>());

// Configure Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});

// AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Entity Framework Core
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    options.UseChangeTrackingProxies(); // Enables change tracking proxies
    options.UseLazyLoadingProxies();    // Enables lazy loading proxies
});


// Register RepositoryBase and ClientRepository
builder.Services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
builder.Services.AddScoped<IClientRepository, ClientRepository>();

// Register BillingService
builder.Services.AddScoped<IBillingService, BillingService>();

// Register MapperService
builder.Services.AddScoped<IMapperService, MapperService>();

// Custom services
builder.Services.AddScoped<IValidatorService, ValidatorService>();

// Enregistrement des validateurs
builder.Services.AddScoped<IValidator<ClientDTO>, ClientDTOValidator>();
builder.Services.AddScoped<IValidator<MealDTO>, MealDTOValidator>();
builder.Services.AddScoped<IValidator<CreditRequestDTO>, CreditRequestDTOValidator>();
builder.Services.AddScoped<IValidator<PayMealRequestDTO>, PayMealRequestDTOValidator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.UseStaticFiles();

// Configure Swagger middleware
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    c.RoutePrefix = string.Empty; // Serve the Swagger UI at the app's root
});

app.Run();
