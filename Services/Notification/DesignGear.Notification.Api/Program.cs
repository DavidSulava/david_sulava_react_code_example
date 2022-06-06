using DesignGear.Contracts.Models.Notification;
using DesignGear.Notification.Api.Communicators;
using DesignGear.Notification.Api.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOptions<EmailOptions>().Bind(builder.Configuration.GetSection("Email")).ValidateDataAnnotations();
builder.Services.AddScoped<EmailCommunicator>();

var app = builder.Build();

app.MapPost("/email", async (EmailCommunicator emailCommunicator, EmailRequestModel request) => {
    await emailCommunicator.SendEmailAsync(request);       
});

app.Run();