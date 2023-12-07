using Domain.Common.DTOs;
using FastEndpoints;
using HighScore.DataAccess;

namespace HighScore.API.Endpoints.GetAllForUser;

public class Handler(IHighScoreRepository repository) : Endpoint<Request, Response>
{
    public override void Configure()
    {
        Get("/{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request request, CancellationToken cancellationToken)
    {
        var scores = await repository.GetScoresForUserAsync(request.Id, cancellationToken);
        var dtos = 
            scores.Select(score => 
                new ScoreDto(
                       score.Value, 
                       score.UserId, 
                       score.Created
                    ));
        await SendAsync(new Response()
        {
            Id = request.Id,
            HighScores = dtos
        }, cancellation: cancellationToken);
    }
}