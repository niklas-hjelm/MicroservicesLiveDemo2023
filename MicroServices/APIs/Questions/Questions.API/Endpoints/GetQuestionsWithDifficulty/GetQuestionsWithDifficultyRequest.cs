using Domain.Common.Enums;

namespace Questions.API.Endpoints.GetQuestionsWithDifficulty;

public class GetQuestionsWithDifficultyRequest
{
    public Difficulty Difficulty { get; set; }
    public int Amount { get; set; }
}