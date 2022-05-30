using Autofac;
using Autofac.Extensions.DependencyInjection;
using DesignGear.Contractor.Api.Config;
using DesignGear.Contractor.Core.Data;
using DesignGear.Contractor.Core.Helpers;
using DesignGear.Contracts.Helpers;
using Kendo.Mvc.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile($"appsettings.Local.json", optional: true);

// Add services to the container.
builder.Services.AddDbContext<DataContext>(
    options => options.UseSqlServer("name=ConnectionStrings:DefaultConnection"));

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(builder =>
    {
        builder.RegisterModule(new AutofacModule());
    });

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.MapType<DataSourceRequest>(() => new OpenApiSchema { Type = typeof(string).Name });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme.",
        Scheme = "bearer",
        Type = SecuritySchemeType.Http
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement{
    {
        new OpenApiSecurityScheme{
            Reference = new OpenApiReference{
                Id = "Bearer", //The name of the previously defined security scheme.
                Type = ReferenceType.SecurityScheme
            }
        },new List<string>()
    }
    });
});

builder.Services.AddAuthorization(opts => {
    opts.AddPolicy("OrganizationSelected", policy => {
        policy.RequireClaim("OrganizationId");
    });
});

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
//builder.Services.Configure<CommunicatorSettings>(builder.Configuration.GetSection("CommunicatorSettings"));
builder.Services.AddOptions<CommunicatorSettings>().Bind(builder.Configuration.GetSection("CommunicatorSettings")).ValidateDataAnnotations();

builder.Services.AddHttpClient();

builder.Services.AddCors(options => {
    options.AddDefaultPolicy(builder => {
        builder.WithOrigins(
            "http://localhost:3000",
            "https://localhost:3000",
            "http://localhost:3000/",
            "https://localhost:3000/",
            "http://95.170.154.243:8055",
            "http://evraz-auth1:8055");

        builder.WithExposedHeaders("Content-Disposition");
        builder.AllowAnyHeader();
        builder.AllowAnyMethod();
        builder.AllowCredentials();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseCors();

app.UseMiddleware<JwtMiddleware>();

app.MapControllers();

app.Run();
