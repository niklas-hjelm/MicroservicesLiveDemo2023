using System.Text.Json;
using System.Text.Json.Serialization;
using Domain.Common.Enums;
using FastEndpoints;
using Questions.DataAccess;

namespace Questions.API.Endpoints.SeedDatabaseFromOpenTriviaDb;

public class SeedDatabaseFromOpenTriviaDbEndpoint(IQuestionRepository repository, IHttpClientFactory httpClientFactory) : Endpoint<SeedDatabaseFromOpenTriviaDbRequest, SeedDatabaseFromOpenTriviaDbResponse>
{
    public override void Configure()
    {
        Post("/seed");
        AllowAnonymous();
    }

    public override async Task HandleAsync(SeedDatabaseFromOpenTriviaDbRequest seedDatabaseFromOpenTriviaDbRequest, CancellationToken cancellationToken)
    {
        using (var client = httpClientFactory.CreateClient("openTriviaDb"))
        {
            
            var response = await client.GetAsync("api.php?amount=50");
            var result = await response.Content.ReadFromJsonAsync<OpenTriviaDbResponse>();
            if (result is {ResponseCode: 0, Results.Count: > 0})
            {
                await repository.AddMultipleAsync(
                    result.Results.Select
                        (
                            q => new Question()
                            {
                                Category = q.Category!,
                                CorrectAnswer = q.CorrectAnswer!,
                                Difficulty = Enum.Parse<Difficulty>(q.Difficulty!, true),
                                IncorrectAnswers = q.IncorrectAnswers!.ToArray(),
                                QuestionText = q.Question!,
                                Type = Enum.Parse<QuestionType>(q.Type!, true)
                            }
                        ),
                    cancellationToken
                    );
            }   
        }


        await SendAsync(
            new SeedDatabaseFromOpenTriviaDbResponse(), 
            cancellation: cancellationToken
            );
    }
}

public class OpenTriviaDbResponse
{
    [JsonPropertyName("response_code")]
    public int ResponseCode { get; set; }

    public ICollection<OpenTriviaDbQuestion>? Results { get; set; }
}

public class OpenTriviaDbQuestion
{
    public string? Category { get; set; }

    public string? Type { get; set; }

    public string? Question { get; set; }

    public string? Difficulty { get; set; }

    [JsonPropertyName("correct_answer")]
    public string? CorrectAnswer { get; set; }

    [JsonPropertyName("incorrect_answers")]
    public ICollection<string>? IncorrectAnswers { get; set; }
}