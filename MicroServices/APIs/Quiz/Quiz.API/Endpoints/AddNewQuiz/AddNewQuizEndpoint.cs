using FastEndpoints;
using Quiz.DataAccess;

namespace Quiz.API.Endpoints.AddNewQuiz;

public class AddNewQuizEndpoint(IQuizRepository quizRepository) : Endpoint<AddNewQuizRequest,AddNewQuizResponse>
{
    public override void Configure()
    {
        Post("add-new-quiz");
        AllowAnonymous();
    }

    public override async Task HandleAsync(AddNewQuizRequest addNewQuizRequest, CancellationToken cancellationToken)
    {
        var quiz = new QuizEntity()
        {
            Title = addNewQuizRequest.NewQuiz!.Title,
            Description = addNewQuizRequest.NewQuiz.Description,
            Author = addNewQuizRequest.NewQuiz.Author,
            CreatedAt = DateTime.UtcNow,
            Categories = addNewQuizRequest.NewQuiz.Categories,
            Questions = addNewQuizRequest.NewQuiz.Questions
        };

        await quizRepository.AddAsync(quiz);

        await SendAsync(new AddNewQuizResponse(), cancellation: cancellationToken);
    }
}