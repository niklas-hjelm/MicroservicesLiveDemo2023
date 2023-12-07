using Domain.Common.DTOs;

namespace Quiz.API.Endpoints.GetAllQuizzes;

public class GetAllQuizzesResponse
{
    public IEnumerable<QuizDto>? Quizzes { get; set; }
}