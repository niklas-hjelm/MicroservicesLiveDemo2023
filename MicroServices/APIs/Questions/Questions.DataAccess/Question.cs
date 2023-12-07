using Domain.Common.Enums;
using Domain.Common.Interfaces.DataAccess;
using MongoDB.Bson;

namespace Questions.DataAccess;

public class Question : IEntity<ObjectId>
{
    public ObjectId Id { get; init; }
    public QuestionType Type { get; init; }
    public Difficulty Difficulty { get; init; }
    public string Category { get; init; } = string.Empty;
    public string QuestionText {get; init;} = string.Empty;
    public string CorrectAnswer {get; init;} = string.Empty;
    public string[] IncorrectAnswers { get; init; }
}