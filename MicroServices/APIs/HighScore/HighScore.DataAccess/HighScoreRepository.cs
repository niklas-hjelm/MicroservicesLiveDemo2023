using Microsoft.EntityFrameworkCore;

namespace HighScore.DataAccess;

public class HighScoreRepository(HighScoreContext context) : IHighScoreRepository
{
    public async Task<Score> GetByIdAsync(int id)
    {
        return await context.Scores.FindAsync(id) ?? throw new InvalidOperationException();
    }

    public async Task<IEnumerable<Score>> GetAllAsync()
    {
        return await context.Scores.ToListAsync();
    }

    public async Task AddAsync(Score entity)
    {
        await context.Scores.AddAsync(entity);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Score entity)
    {
        context.Scores.Update(entity);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await context.Scores.FindAsync(id);
        if (entity != null)
        {
            context.Scores.Remove(entity);
            await context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Score>> GetTopScoresAsync(int count, CancellationToken cancellationToken)
    {
        var scores = 
            await context.Scores.OrderByDescending(x => x.Value)
                .Take(count)
                .ToListAsync(cancellationToken);
        return scores;
    }

    public async Task<IEnumerable<Score>> GetScoresForUserAsync(Guid userId, CancellationToken cancellationToken)
    {
        var scores = 
            await context.Scores.Where(x => x.UserId == userId)
                .OrderByDescending(x => x.Value)
                .ToListAsync(cancellationToken);
        return scores;
    }
}