using Domain.Common.Enums;
using Domain.Common.Interfaces.DataAccess;
using MongoDB.Bson;

namespace Questions.DataAccess;

public interface IQuestionRepository : IGenericRepository<Question, ObjectId>
{
    Task<IEnumerable<Question>> GetQuestionsWithCategory(
        string category, 
        int count,
        CancellationToken cancellationToken
    );

    Task<IEnumerable<Question>> GetQuestionsForDifficultyAsync(
        Difficulty difficulty, 
        int count, 
        CancellationToken cancellationToken
    );

    Task<IEnumerable<Question>> GetQuestionsForQuizAsync(
        IEnumerable<string> ids, 
        CancellationToken cancellationToken
    );

    Task AddMultipleAsync(
        IEnumerable<Question> questions, 
        CancellationToken cancellationToken
    );
}