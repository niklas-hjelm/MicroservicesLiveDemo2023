using Domain.Common.Interfaces.DataAccess;

namespace HighScore.DataAccess;

public interface IHighScoreRepository : IGenericRepository<Score, int>
{
    Task<IEnumerable<Score>> GetTopScoresAsync(int count, CancellationToken cancellationToken);
    Task<IEnumerable<Score>> GetScoresForUserAsync(Guid userId, CancellationToken cancellationToken);
}