using FluentValidation;
using MarketingMailer.API.Endpoints;
using MarketingMailer.API.Services;
using MarketingMailer.API.Validators;
using MarketingMailer.Application.Interfaces;
using MarketingMailer.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

// Registering Singleton services
builder.Services.AddSingleton<IEmailService, EmailService>();
builder.Services.AddSingleton<IEmailQueue, EmailQueue>();

// registering the EmailBackgroundService
builder.Services.AddHostedService<EmailBackgroundService>();

// Registering FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<SendMarketingRequestValidator>();

// Registering ProcessServices
AddProcessServices(builder.Services);

var app = builder.Build();

// Register the HTTP endpoints
app.MapGet("/", () => "Welcome to the Marketing Mailer API!");
app.AddMapEmailEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();



app.Run();
return;

//Process Services
void AddProcessServices(IServiceCollection services)
{
    //--MarketingMailer
    builder.Services.AddKeyedScoped<IHttpProcessService, SendMarketingEmailService>(string.Concat("MarketingMailer","SendMarketingEmailService"));
    
}