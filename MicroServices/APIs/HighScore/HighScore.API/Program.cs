global using FastEndpoints;
using HighScore.DataAccess;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("HighScoreDb");

builder.Services.AddSqlServer<HighScoreContext>(connectionString);
builder.Services.AddFastEndpoints();
builder.Services.AddScoped<IHighScoreRepository, HighScoreRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseFastEndpoints();

app.Run();

