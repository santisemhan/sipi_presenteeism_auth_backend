using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using SIPI_PRESENTEEISM.Core.Helpers.Middleware;
using SIPI_PRESENTEEISM.Core.Integrations.Azure;
using SIPI_PRESENTEEISM.Core.Integrations.Interfaces;
using SIPI_PRESENTEEISM.Core.Repositories;
using SIPI_PRESENTEEISM.Core.Repositories.Interfaces;
using SIPI_PRESENTEEISM.Core.Services;
using SIPI_PRESENTEEISM.Core.Services.Interfaces;
using SIPI_PRESENTEEISM.Data;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

var services = builder.Services;

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
});

services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

// Singletons for Injection Dependencies
// Services
services.AddScoped<ICognitiveService, CognitiveService>();

// Repositories
services.AddScoped<IStamentRepository, StamentRepository>();
services.AddScoped<IEmployeeRepository, EmployeeRepository>();

// Integrations
services.AddScoped<IFaceRecognition, AzureFaceRecognition>();

services.AddEndpointsApiExplorer();

services.AddLogging(loggingBuilder =>
{
    loggingBuilder.AddConsole().AddFilter(DbLoggerCategory.Database.Command.Name, LogLevel.Information);
    loggingBuilder.AddDebug();
});

services.AddLocalization(options => { options.ResourcesPath = "Resources"; });

services.AddHttpContextAccessor();

services.Configure<RequestLocalizationOptions>(opts => {
    var supportedCultures = new List<CultureInfo> { new CultureInfo("es") };

    opts.DefaultRequestCulture = new RequestCulture("es");
    // Formatting numbers, dates, etc.
    opts.SupportedCultures = supportedCultures;
    // UI strings that we have localized.
    opts.SupportedUICultures = supportedCultures;
});

services.AddCors(options =>
{
    options.AddPolicy("_AllowOriginDev", builder =>
    {
        builder.WithOrigins("*")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .WithExposedHeaders("Content-Disposition")
                .AllowCredentials();
    });   
});

var app = builder.Build();
var environment = app.Environment;

app.UseMiddleware<ExceptionHandler>();

// Configure the HTTP request pipeline.
// Configure the HTTP request pipeline.
if (environment.IsDevelopment() || environment.IsEnvironment("Local"))
{
    app.UseSwagger();
    app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "SIPI Authorization"); });
    app.UseCors("_AllowOriginDev");
}
else if (environment.IsProduction())
{
    app.UseCors("_AllowOriginDev");
}
else
{
    throw new Exception("Invalid environment");
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
