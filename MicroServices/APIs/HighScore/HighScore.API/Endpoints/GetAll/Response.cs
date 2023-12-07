using Domain.Common.DTOs;

namespace HighScore.API.Endpoints.GetAll;

public class Response
{
    public IEnumerable<ScoreDto> HighScores { get; set; } = new List<ScoreDto>();
}