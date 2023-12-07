using Domain.Common.DTOs;
using FastEndpoints;
using Quiz.DataAccess;

namespace Quiz.API.Endpoints.GetQuizzesForAuthor;

public class GetQuizzesForAuthorEndpoint(IQuizRepository repository) : Endpoint<GetQuizzesForAuthorRequest, GetQuizzesForAuthorResponse>
{
    public override void Configure()
    {
        Get("author/{authorId}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetQuizzesForAuthorRequest getQuizzesForAuthorRequest, CancellationToken cancellationToken)
    {
        var quizzes = await repository.GetQuizzesForAuthorAsync(getQuizzesForAuthorRequest.AuthorId, cancellationToken);
        await SendAsync(
           new GetQuizzesForAuthorResponse()
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