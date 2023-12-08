using Domain.Common.Enums;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Quiz.DataAccess;

public class QuizRepository : IQuizRepository
{
    private readonly IMongoCollection<QuizEntity> _collection;

    public QuizRepository()
    {
        var hostname = Environment.GetEnvironmentVariable("DB_HOST");
        var databaseName = Environment.GetEnvironmentVariable("DB_DATABASE");
        var connectionString = $"mongodb://{hostname}:27017";

        var client = new MongoClient(connectionString);
        var database = client.GetDatabase(databaseName);
        _collection = database.GetCollection<QuizEntity>("Quizzes", new MongoCollectionSettings() { AssignIdOnInsert = true });
    }

    public async Task<QuizEntity> GetByIdAsync(ObjectId id)
    {
        var filter = Builders<QuizEntity>.Filter.Eq("_id", id);
        return await _collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<QuizEntity>> GetAllAsync()
    {
        return await _collection.Find(new BsonDocument()).ToListAsync();
    }

    public async Task AddAsync(QuizEntity entity)
    {
        await _collection.InsertOneAsync(entity);
    }

    public async Task UpdateAsync(QuizEntity entity)
    {
        var filter = Builders<QuizEntity>.Filter.Eq("_id", entity.Id);
        await _collection.ReplaceOneAsync(filter, entity);
    }

    public async Task DeleteAsync(ObjectId id)
    {
        var filter = Builders<QuizEntity>.Filter.Eq("_id", id);
        await _collection.DeleteOneAsync(filter);
    }

    public async Task AddQuestionAsync(ObjectId quizId, string? questionId, CancellationToken cancellationToken)
    {
        var filter = Builders<QuizEntity>.Filter.Eq("_id", quizId);
        var update = Builders<QuizEntity>.Update.AddToSet(x => x.Questions, questionId);
        await _collection.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);
    }

    public async Task RemoveQuestionAsync(ObjectId quizId, string questionId, CancellationToken cancellationToken)
    {
        var filter = Builders<QuizEntity>.Filter.Eq("_id", quizId);
        var update = Builders<QuizEntity>.Update.Pull(x => x.Questions, questionId);
        await _collection.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);
    }

    public async Task<IEnumerable<QuizEntity>> GetQuizzesForCategoryAsync(string category, CancellationToken cancellationToken)
    {
        var filter = Builders<QuizEntity>.Filter.Eq("Categories", category);
        return await _collection.Find(filter).ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<QuizEntity>> GetQuizzesForAuthorAsync(string author, CancellationToken cancellationToken)
    {
        var filter = Builders<QuizEntity>.Filter.Eq("Author", author);
        return await _collection.Find(filter).ToListAsync(cancellationToken);
    }
}