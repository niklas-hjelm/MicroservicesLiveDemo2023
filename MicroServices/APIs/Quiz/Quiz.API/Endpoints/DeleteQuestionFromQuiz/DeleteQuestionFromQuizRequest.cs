namespace Quiz.API.Endpoints.DeleteQuestionFromQuiz;

public class DeleteQuestionFromQuizRequest
{
    public string? QuizId { get; set; }
    public string? QuestionId { get; set; }
}