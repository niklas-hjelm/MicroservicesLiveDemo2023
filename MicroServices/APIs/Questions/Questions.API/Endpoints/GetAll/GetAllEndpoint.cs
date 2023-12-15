using Domain.Common.DTOs;
using Domain.Common.RabbitMq;
using FastEndpoints;
using Questions.DataAccess;

namespace Questions.API.Endpoints.GetAll;

public class GetAllEndpoint(IQuestionRepository repository, IMessageProducerService messageProducer) : Endpoint<GetAllRequest, GetAllResponse>
{
    public override void Configure()
    {
        Get("/");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetAllRequest req, CancellationToken ct)
    {
        await messageProducer.SendMessageAsync("God Jul!");

        var allQuestions = await repository.GetAllAsync();
        var questionDtos = 
            allQuestions.
                Select(
                    q => 
                        new QuestionDto(
                            q.Type, 
                            q.Difficulty, 
                            q.Category, 
                            q.QuestionText, 
                            q.CorrectAnswer, 
                            q.IncorrectAnswers
                            )
                    );
        await SendAsync(
                new GetAllResponse()
                {
                    Questions = questionDtos
                },
                cancellation: ct
            );
    }
}