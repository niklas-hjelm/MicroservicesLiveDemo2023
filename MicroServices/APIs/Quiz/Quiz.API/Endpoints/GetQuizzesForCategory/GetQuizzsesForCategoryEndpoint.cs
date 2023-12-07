using Domain.Common.DTOs;
using FastEndpoints;
using Quiz.DataAccess;

namespace Quiz.API.Endpoints.GetQuizzesForCategory;

public class GetQuizzesForCategoryEndpoint(IQuizRepository repository) : Endpoint<GetQuizzesForCategoryRequest, GetQuizzesForCategoryResponse>
{
    public override void Configure()
    {
        Get("category/{categoryId}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetQuizzesForCategoryRequest getQuizzesForCategoryRequest, CancellationToken cancellationToken)
    {
        var quizzes = await repository.GetQuizzesForCategoryAsync(getQuizzesForCategoryRequest.CategoryId, cancellationToken);
        await SendAsync(
              new GetQuizzesForCategoryResponse()
              {
                  Quizzes = quizzes.Select(quiz =>
                        new QuizDto(
                            quiz.Title,
                            quiz.Description,
                            quiz.Author,
                            quiz.CreatedAt,
                            quiz.Categories,
                            quiz.Questions
                        )).ToList()
              },
              cancellation: cancellationToken
        );
    }
}