using FastEndpoints;
using MongoDB.Bson;
using Quiz.DataAccess;

namespace Quiz.API.Endpoints.DeleteQuestionFromQuiz;

public class DeleteQuestionFromQuizEndpoint(IQuizRepository repository) : Endpoint<DeleteQuestionFromQuizRequest,DeleteQuestionFromQuizResponse>
{
    public override void Configure()
    {
        Delete("/delete-question-from-quiz");
        AllowAnonymous();
    }

    public override async Task HandleAsync(DeleteQuestionFromQuizRequest deleteQuestionFromQuizRequest, CancellationToken cancellationToken)
    {
        await repository.RemoveQuestionAsync(ObjectId.Parse(deleteQuestionFromQuizRequest.QuizId), deleteQuestionFromQuizRequest.QuestionId, cancellationToken);
        await SendAsync(
            new DeleteQuestionFromQuizResponse()
            {

            }, 
            cancellation: cancellationToken
            );
    }
}