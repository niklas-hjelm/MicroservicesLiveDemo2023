using Domain.Common.DTOs;

namespace Quiz.API.Endpoints.GetQuizzesForAuthor;

public class GetQuizzesForAuthorResponse
{
    public IEnumerable<QuizDto> Quizzes { get; set; } = null!;
}