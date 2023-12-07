using Domain.Common.DTOs;

namespace HighScore.API.Endpoints.GetTopFive;

public class Response
{
    public IEnumerable<ScoreDto> TopFiveScores { get; set; } = new List<ScoreDto>();
}