using Domain.Common.DTOs;
using FastEndpoints;
using MongoDB.Bson;
using Quiz.DataAccess;

namespace Quiz.API.Endpoints.AddQuestionToQuiz;

public class AddQuestionToQuizEnpoint(IQuizRepository quizRepository) : Endpoint<AddQuestionToQuizRequest, AddQuestionToQuizResponse>
{
    public override void Configure()
    {
        Post("/add-question-to-quiz");
        AllowAnonymous();
    }

    public override async Task HandleAsync(AddQuestionToQuizRequest addQuestionToQuizRequest, CancellationToken cancellationToken)
    {
        await quizRepository.AddQuestionAsync(ObjectId.Parse(addQuestionToQuizRequest.QuizId), addQuestionToQuizRequest.QuestionId, cancellationToken);
        await SendAsync(
            new AddQuestionToQuizResponse()
            {
            },
            cancellation: cancellationToken
            );
    }
}