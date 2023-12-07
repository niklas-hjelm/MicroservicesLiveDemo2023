using Domain.Common.DTOs;

namespace Questions.API.Endpoints.GetQuestionsForQuiz;

public class GetQuestionsForQuizResponse
{
    public IEnumerable<QuestionDto> Questions { get; set; } = new List<QuestionDto>();
}