using FastEndpoints;
using Questions.DataAccess;

var builder = WebApplication.CreateBuilder(args);

// Register Services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddFastEndpoints();
builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
builder.Services.AddHttpClient("openTriviaDb", c => c.BaseAddress = new System.Uri("https://opentdb.com/"));

var app = builder.Build();

// Middleware pipeline

app.UseHttpsRedirection();
app.UseRouting();

app.UseFastEndpoints();

app.Run();
