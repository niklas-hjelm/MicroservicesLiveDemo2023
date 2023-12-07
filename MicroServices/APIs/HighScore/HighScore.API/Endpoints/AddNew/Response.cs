using Domain.Common.DTOs;

namespace HighScore.API.Endpoints.AddNew;

public readonly struct Response(string message, bool success, ScoreDto? data)
{
    public string Message { get; } = message;
    public bool Success { get; } = success;
    public ScoreDto? Data { get; } = data;
}