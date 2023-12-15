using Domain.Common.RabbitMq;
using FastEndpoints;
using Questions.API.Services;
using Questions.DataAccess;

var builder = WebApplication.CreateBuilder(args);

// Register Services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddFastEndpoints();
builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
builder.Services.AddHttpClient("openTriviaDb", c => c.BaseAddress = new System.Uri("https://opentdb.com/"));

builder.Services.AddScoped(sp => new RabbitMqConfiguration()
{
    HostName = "rabbitmq",
    Username = "guest",
    Password = "guest"
});

builder.Services.AddScoped<IMessageProducerService, MessageProducerService>();

var app = builder.Build();

// Middleware pipeline
app.UseRouting();

app.UseFastEndpoints();

app.Run();
