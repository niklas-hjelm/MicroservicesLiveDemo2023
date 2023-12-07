using FastEndpoints;
using MongoDB.Bson;
using Quiz.DataAccess;

namespace Quiz.API.Endpoints.DeleteQuiz;

public class DeleteQuizEndpoint(IQuizRepository repository) : Endpoint<DeleteQuizRequest, DeleteQuizResponse>
{
    public override void Configure()
    {
        Delete("{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(DeleteQuizRequest deleteQuizRequest, CancellationToken cancellationToken)
    {
        await repository.DeleteAsync(ObjectId.Parse(deleteQuizRequest.QuizId));
        await SendAsync(
                   new DeleteQuizResponse()
                   {

                   },
                   cancellation: cancellationToken
                   );
    }
}