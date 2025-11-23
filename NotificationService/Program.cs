using Microsoft.EntityFrameworkCore;
using NotificationService.Data;
using NotificationService.Services;
using NotificationService.Messaging;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext
var connectionString = builder.Configuration.GetConnectionString("NotificationDb");
builder.Services.AddDbContext<NotificationDbContext>(options =>
    options.UseSqlServer(connectionString));

// Register services
builder.Services.AddScoped<INotificationService, NotificationService.Services.NotificationService>();

// Register RabbitMQ subscriber as singleton
builder.Services.AddSingleton<RabbitMQSubscriber>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Start RabbitMQ subscriber
var subscriber = app.Services.GetRequiredService<RabbitMQSubscriber>();
subscriber.Start();

app.Run();