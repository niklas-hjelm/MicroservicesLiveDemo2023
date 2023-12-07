using Domain.Common.DTOs;
using HighScore.DataAccess;

namespace HighScore.API.Endpoints.AddNew;

public class Handler(IHighScoreRepository repository) : Endpoint<Request, Response>
{
    public override void Configure()
    {
        Post("/");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request request, CancellationToken cancellationToken)
    {
        if (request.Data is null)
        {
            await SendAsync(new Response("Fail", false, null), 400, CancellationToken.None);
            return;
        }

        await repository.AddAsync(
                new Score()
                {
                    Value = request.Data.Score,
                    UserId = request.Data.PlayerId,
                    Created = DateTime.UtcNow
                }
            );

        await SendAsync(new Response("DataAdded", true, request.Data), cancellation: cancellationToken);

    }
}