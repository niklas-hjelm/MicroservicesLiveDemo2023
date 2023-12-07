using Domain.Common.DTOs;
using FastEndpoints;
using Quiz.DataAccess;

namespace Quiz.API.Endpoints.GetAllQuizzes;

public class GetAllQuizzesEndpoint(IQuizRepository quizRepository) : Endpoint<GetAllQuizzesRequest, GetAllQuizzesResponse>
{
    public override void Configure()
    {
        Get("/");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetAllQuizzesRequest getAllQuizzesRequest, CancellationToken cancellationToken)
    {
        var allQuizzes = await quizRepository.GetAllAsync();
        var dtos = allQuizzes.Select(
            quiz =>
                new QuizDto(
                    quiz.Title,
                    quiz.Description,
                    quiz.Author,
                    quiz.CreatedAt,
                    quiz.Categories,
                    quiz.Questions)
        );

        await SendAsync(
            new GetAllQuizzesResponse()
            {
                Quizzes = dtos
            },
            cancellation: cancellationToken
        );
    }
}