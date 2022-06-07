using Autofac;
using Autofac.Extensions.DependencyInjection;
using DesignGear.ConfigManager.Api.Config;
using DesignGear.ConfigManager.Core.Data;
using DesignGear.ConfigManager.Core.Jobs;
using DesignGear.Contracts.Helpers;
using Hangfire;
using Hangfire.SqlServer;
using Kendo.Mvc.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
//builder.Configuration.AddJsonFile($"appsettings.Local.json", optional: true);

// Add services to the container.
builder.Services.AddDbContext<DataContext>(
    options => options.UseSqlServer("name=ConnectionStrings:DefaultConnection"));

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(builder =>
    {
        builder.RegisterModule(new AutofacModule());
    });

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddOptions<CommunicatorSettings>().Bind(builder.Configuration.GetSection("CommunicatorSettings")).ValidateDataAnnotations();

builder.Services.AddHttpClient();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.MapType<DataSourceRequest>(() => new OpenApiSchema { Type = typeof(string).Name });
});

builder.Services.AddHangfire(c => c
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseSqlServerStorage(builder.Configuration.GetConnectionString("HangfireConnection"), new SqlServerStorageOptions
    {
        CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
        SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
        QueuePollInterval = TimeSpan.Zero,
        UseRecommendedIsolationLevel = true,
        DisableGlobalLocks = true
    }));
builder.Services.AddHangfireServer();

//JobStorage.Current = new SqlServerStorage(builder.Configuration.GetConnectionString("HangfireConnection"), new SqlServerStorageOptions
//{
//    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
//    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
//    QueuePollInterval = TimeSpan.Zero,
//    UseRecommendedIsolationLevel = true,
//    DisableGlobalLocks = true
//});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
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

//Do migration
using (var scope = app.Services.CreateScope()) {
    var dataContext = scope.ServiceProvider.GetRequiredService<DataContext>();
    dataContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.UseHangfireDashboard();

app.MapControllers();

//RecurringJob.AddOrUpdate<ConfigurationPushingJob>("Pushing data to Forge", (x) => x.Do(), Cron.Hourly());// "0 */1 * ? * *");
//RecurringJob.AddOrUpdate<ConfigurationPullingJob>("Pulling data from Forge", (x) => x.Do(), Cron.Hourly());// "0 */1 * ? * *");

app.Run();
