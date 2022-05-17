using Autofac;
using Autofac.Extensions.DependencyInjection;
using DesignGear.ConfigManager.Api.Config;
using DesignGear.ConfigManager.Core.Data;
using DesignGear.ConfigManager.Core.Jobs;
using Hangfire;
using Microsoft.EntityFrameworkCore;

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

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//RecurringJob.AddOrUpdate<ConfigurationPushingJob>("Pushing configurations to inventor", (x) => x.Do(), "0 */1 * ? * *");
//RecurringJob.AddOrUpdate<ConfigurationPullingJob>("Pulling configurations from inventor", (x) => x.Do(), "0 */1 * ? * *");

builder.Services.AddCors(options => {
    options.AddDefaultPolicy(builder => {
        builder.WithOrigins(
            "http://localhost:3000",
            "https://localhost:3000",
            "http://localhost:3000/",
            "https://localhost:3000/");
        

        builder.WithExposedHeaders("Content-Disposition");
        builder.AllowAnyHeader();
        builder.AllowAnyMethod();
        builder.AllowCredentials();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
