using Domain.Common.DTOs;

namespace Quiz.API.Endpoints.AddNewQuiz;

public class AddNewQuizRequest
{
    public QuizDto? NewQuiz { get; set; }
}