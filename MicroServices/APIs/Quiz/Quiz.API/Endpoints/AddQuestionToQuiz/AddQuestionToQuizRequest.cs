namespace Quiz.API.Endpoints.AddQuestionToQuiz;

public class AddQuestionToQuizRequest
{
    public string? QuizId { get; set; }
    public string? QuestionId { get; set; }
}