using Domain.Common.DTOs;
using FastEndpoints;
using Questions.DataAccess;

namespace Questions.API.Endpoints.GetQuestionsFromCategory;

public class GetQuestionsFromCategoryEndpoint(IQuestionRepository repository) : Endpoint<GetQuestionsFromCategoryRequest, GetQuestionsFromCategoryResponse>
{
    public override void Configure()
    {
        Get("/category");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetQuestionsFromCategoryRequest getQuestionsFromCategoryRequest, CancellationToken cancellationToken)
    {
        var questions = await repository.GetQuestionsWithCategory(getQuestionsFromCategoryRequest.Category, getQuestionsFromCategoryRequest.Amount, cancellationToken);
        await SendAsync(new GetQuestionsFromCategoryResponse()
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