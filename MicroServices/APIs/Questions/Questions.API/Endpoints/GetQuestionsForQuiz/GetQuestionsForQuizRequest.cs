using Domain.Common.Enums;

namespace Questions.API.Endpoints.GetQuestionsForQuiz;

public class GetQuestionsForQuizRequest
{
    public IEnumerable<string> Ids { get; set; } = new List<string>();
}