using Domain.Common.DTOs;
using FastEndpoints;
using Questions.DataAccess;

namespace Questions.API.Endpoints.GetQuestionsForQuiz;

public class GetQuestionsForQuizEndpoint(IQuestionRepository repository) : Endpoint<GetQuestionsForQuizRequest, GetQuestionsForQuizResponse>
{
    public override void Configure()
    {
        Get("/forQuiz");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetQuestionsForQuizRequest getQuestionsForQuizRequest, CancellationToken cancellationToken)
    {
        var questions = await repository.GetQuestionsForQuizAsync(getQuestionsForQuizRequest.Ids, cancellationToken);
        await SendAsync(new GetQuestionsForQuizResponse()
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