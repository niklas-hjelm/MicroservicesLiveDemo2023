using Domain.Common.Interfaces.DataAccess;
using MongoDB.Bson;

namespace Quiz.DataAccess;

public class QuizEntity : IEntity<ObjectId>
{
    public ObjectId Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public ICollection<string> Categories { get; set; } = new List<string>();
    public ICollection<string> Questions { get; set; } = new List<string>();
}