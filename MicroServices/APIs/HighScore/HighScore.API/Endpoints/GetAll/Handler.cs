using Domain.Common.DTOs;
using HighScore.DataAccess;

namespace HighScore.API.Endpoints.GetAll;

public class Handler(IHighScoreRepository repository) : Endpoint<Request, Response>
{
    public override void Configure()
    {
        Get("/");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request request, CancellationToken cancellationToken)
    {
        var scores= await repository.GetAllAsync();
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
                HighScores = dtos
            }, 
            cancellation: cancellationToken);
    }
}