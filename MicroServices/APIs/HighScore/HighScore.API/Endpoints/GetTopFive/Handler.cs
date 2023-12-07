using Domain.Common.DTOs;
using HighScore.DataAccess;

namespace HighScore.API.Endpoints.GetTopFive;

public class Handler(IHighScoreRepository repository) : Endpoint<Request, Response>
{
    public override void Configure()
    {
        Get("/top-five");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request request, CancellationToken cancellationToken)
    {
        var scores = await repository.GetTopScoresAsync(5, cancellationToken);
        var dtos =
            scores.Select(score =>
            new ScoreDto(
                score.Value, 
                score.UserId, 
                score.Created
                )
            );

        await SendAsync(
                new Response()
                {
                    TopFiveScores = dtos
                },
                cancellation: cancellationToken
            );
    }
}