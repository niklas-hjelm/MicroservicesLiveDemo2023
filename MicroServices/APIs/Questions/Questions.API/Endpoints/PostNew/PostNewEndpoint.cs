using FastEndpoints;
using Questions.DataAccess;

namespace Questions.API.Endpoints.PostNew;

public class PostNewEndpoint(IQuestionRepository repository) : Endpoint<PostNewRequest, PostNewResponse>
{
    public override void Configure()
    {
        Post("/");
        AllowAnonymous();
    }

    public override async Task HandleAsync(PostNewRequest postNewRequest, CancellationToken cancellationToken)
    {
        await repository.AddAsync(new Question()
        {
            Category = postNewRequest.NewQuestion.Category, 
            CorrectAnswer = postNewRequest.NewQuestion.CorrectAnswer,
            Difficulty = postNewRequest.NewQuestion.Difficulty,
            IncorrectAnswers = postNewRequest.NewQuestion.IncorrectAnswers,
            QuestionText = postNewRequest.NewQuestion.QuestionText,
            Type = postNewRequest.NewQuestion.Type
        });

        await SendAsync(new PostNewResponse()
        {

        }, cancellation: cancellationToken);
    }
}