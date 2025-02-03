using Cantine.Core.Interfaces.Mappings;
using Cantine.Core.Interfaces.Repositories;
using Cantine.Core.Interfaces.Validation;
using Cantine.Core.Mappings;
using Cantine.Core.Validations;
using Cantine.Infrastructure.Data;
using Cantine.Infrastructure.Repositories;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers()
            .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ClientDTOValidator>());

        // Configure Swagger/OpenAPI
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // AutoMapper
        builder.Services.AddAutoMapper(typeof(MappingProfile));

        // Entity Framework Core
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        // Register RepositoryBase and ClientRepository
        builder.Services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
        builder.Services.AddScoped<IClientRepository, ClientRepository>();

        // Register MapperService
        builder.Services.AddScoped<IMapperService, MapperService>();

        // Custom services
        builder.Services.AddScoped<IValidatorService, ValidatorService>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}
