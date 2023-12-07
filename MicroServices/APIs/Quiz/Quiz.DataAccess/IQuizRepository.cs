using Domain.Common.Enums;
using Domain.Common.Interfaces.DataAccess;
using MongoDB.Bson;

namespace Quiz.DataAccess;

public interface IQuizRepository : IGenericRepository<QuizEntity, ObjectId>
{
    Task AddQuestionAsync(ObjectId quizId, string? questionId, CancellationToken cancellationToken);
    Task RemoveQuestionAsync(ObjectId quizId, string questionId, CancellationToken cancellationToken);
    Task<IEnumerable<QuizEntity>> GetQuizzesForCategoryAsync(string category, CancellationToken cancellationToken);
    Task<IEnumerable<QuizEntity>> GetQuizzesForAuthorAsync(string author, CancellationToken cancellationToken);
}