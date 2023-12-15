using Domain.Common.RabbitMq;
using FastEndpoints;
using Quiz.API.BackgroundServices;
using Quiz.API.Services;
using Quiz.API.Services.Interfaces;
using Quiz.DataAccess;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddFastEndpoints();
builder.Services.AddScoped<IQuizRepository, QuizRepository>();

builder.Services.AddSingleton(sp => new RabbitMqConfiguration()
{
    HostName = "rabbitmq",
    Username = "guest",
    Password = "guest"
});

builder.Services.AddSingleton<IRabbitMqService, RabbitMqService>();
builder.Services.AddSingleton<IMessageConsumerService, MessageConsumerService>();
builder.Services.AddHostedService<MessageConsumerHostedService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}

app.UseRouting();
app.UseFastEndpoints();
app.Run();