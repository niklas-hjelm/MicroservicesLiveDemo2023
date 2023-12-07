using Domain.Common.DTOs;

namespace Quiz.API.Endpoints.UpdateQuiz;

public class Request
{
    public string QuizId { get; set; } = null!;
    public QuizDto Quiz { get; set; } = null!;
}