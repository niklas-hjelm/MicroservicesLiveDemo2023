using Domain.Common.Interfaces.DataAccess;

namespace HighScore.DataAccess;

public class Score : IEntity<int>
{
    public int Id { get; set; }
    public Guid UserId { get; set; }
    public double Value { get; set; }
    public DateTime Created { get; set; }
}