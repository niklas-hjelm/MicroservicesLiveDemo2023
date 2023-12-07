using Domain.Common.DTOs;

namespace HighScore.API.Endpoints.GetAllForUser;

public class Response
{
    public Guid Id { get; set; }
    public IEnumerable<ScoreDto>? HighScores { get; set; } = new List<ScoreDto>();
}