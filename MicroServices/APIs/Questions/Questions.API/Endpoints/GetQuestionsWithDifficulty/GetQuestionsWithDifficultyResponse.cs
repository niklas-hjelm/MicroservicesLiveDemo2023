using Domain.Common.DTOs;

namespace Questions.API.Endpoints.GetQuestionsWithDifficulty;

public class GetQuestionsWithDifficultyResponse
{
    public IEnumerable<QuestionDto> Questions { get; set; }
}