using Domain.Common.DTOs;

namespace Quiz.API.Endpoints.GetQuizzesForCategory;

public class GetQuizzesForCategoryResponse
{
    public IEnumerable<QuizDto> Quizzes { get; set; } = null!;
}