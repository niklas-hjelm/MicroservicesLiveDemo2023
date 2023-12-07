using Domain.Common.DTOs;

namespace Quiz.API.Endpoints.GetQuiz;

public class GetQuizResponse
{
    public QuizDto? Quiz { get; set; }
}