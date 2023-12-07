using Domain.Common.DTOs;

namespace HighScore.API.Endpoints.AddNew;

public class Request
{
    public ScoreDto? Data { get; init; }
}