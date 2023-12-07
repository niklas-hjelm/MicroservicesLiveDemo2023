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
}