using Domain.Common.DTOs;
using FastEndpoints;
using Questions.DataAccess;

namespace Questions.API.Endpoints.GetQuestionsWithDifficulty;

public class GetQuestionsWithDifficultyEndpoint(IQuestionRepository repository) : Endpoint<GetQuestionsWithDifficultyRequest, GetQuestionsWithDifficultyResponse>
{
    public override void Configure()
    {
        Get("/difficulty");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetQuestionsWithDifficultyRequest getQuestionsWithDifficultyRequest, CancellationToken cancellationToken)
    {
        var questions = await repository.GetQuestionsForDifficultyAsync(getQuestionsWithDifficultyRequest.Difficulty, getQuestionsWithDifficultyRequest.Amount, cancellationToken);
        await SendAsync(new GetQuestionsWithDifficultyResponse()
        {
            Questions = questions.Select(q =>
                new QuestionDto(
                    q.Type,
                    q.Difficulty,
                    q.Category,
                    q.QuestionText,
                    q.CorrectAnswer,
                    q.IncorrectAnswers
                )).ToList()
        }, cancellation: cancellationToken);
    }
}