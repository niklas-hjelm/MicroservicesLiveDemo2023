using System.Net;
using Domain.Common.DTOs;
using FastEndpoints;
using MongoDB.Bson;
using Quiz.DataAccess;

namespace Quiz.API.Endpoints.GetQuiz;

public class GetQuizEndpoint(IQuizRepository repository) : Endpoint<GetQuizRequest, GetQuizResponse>
{
    public override void Configure()
    {
        Get("{quizId}");
    }

    public override async Task HandleAsync(GetQuizRequest getQuizRequest, CancellationToken cancellationToken)
    {
        var quiz = await repository.GetByIdAsync(ObjectId.Parse(getQuizRequest.QuizId));

        await SendAsync(
            new GetQuizResponse()
            {
                Quiz = 
                    new QuizDto(
                        quiz.Title,
                        quiz.Description,
                        quiz.Author,
                        quiz.CreatedAt,
                        quiz.Categories,
                        quiz.Questions
                        )
            }, cancellation: cancellationToken);
    }
}