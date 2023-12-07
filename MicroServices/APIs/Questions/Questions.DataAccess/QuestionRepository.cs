using Domain.Common.Enums;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Questions.DataAccess;

public class QuestionRepository : IQuestionRepository
{
    private readonly IMongoCollection<Question> _collection;

    public QuestionRepository()
    {
        var hostname = "localhost";
        var databaseName = "QuestionDb";
        var connectionString = $"mongodb://{hostname}:27017";

        var client = new MongoClient(connectionString);
        var database = client.GetDatabase(databaseName);
        _collection = database.GetCollection<Question>("Questions", new MongoCollectionSettings() { AssignIdOnInsert = true });
    }


    public async Task<Question> GetByIdAsync(ObjectId id)
    {
        var filter = Builders<Question>.Filter.Eq("_id", id);
        return await _collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Question>> GetAllAsync()
    {
        return await _collection.Find(new BsonDocument()).ToListAsync();
    }

    public async Task AddAsync(Question entity)
    {
        await _collection.InsertOneAsync(entity);
    }

    public async Task UpdateAsync(Question entity)
    {
        var filter = Builders<Question>.Filter.Eq("_id", entity.Id);
        await _collection.ReplaceOneAsync(filter, entity);
    }

    public async Task DeleteAsync(ObjectId id)
    {
        var filter = Builders<Question>.Filter.Eq("_id", id);
        await _collection.DeleteOneAsync(filter);
    }

    public async Task<IEnumerable<Question>> GetQuestionsWithCategory(string category, int count, CancellationToken cancellationToken)
    {
        var rnd = new Random();
        var filter = Builders<Question>.Filter.Eq("Category", category);
        var questionsWithCategory = await _collection.Find(filter).ToListAsync(cancellationToken);
        var skip = rnd.Next(questionsWithCategory.Count - count);
        return questionsWithCategory.Skip(skip).Take(count);
    }

    public async Task<IEnumerable<Question>> GetQuestionsForDifficultyAsync(Difficulty difficulty, int count, CancellationToken cancellationToken)
    {
        var rnd = new Random();
        var filter = Builders<Question>.Filter.Eq("Difficulty", difficulty.ToString());
        var questionsWithDifficulty = await _collection.Find(filter).ToListAsync(cancellationToken);
        var skip = rnd.Next(questionsWithDifficulty.Count - count);
        return questionsWithDifficulty.Skip(skip).Take(count);
    }

    public async Task<IEnumerable<Question>> GetQuestionsForQuizAsync(IEnumerable<string> ids, CancellationToken cancellationToken)
    {
        var filter = Builders<Question>.Filter.In("_id", ids.Select(ObjectId.Parse));
        return await _collection.Find(filter).ToListAsync(cancellationToken);
    }

    public async Task AddMultipleAsync(IEnumerable<Question> questions, CancellationToken cancellationToken)
    {
        var bulkOps = new List<WriteModel<Question>>();
        foreach (var question in questions)
        {
            var filter = Builders<Question>.Filter.Eq(doc => doc.QuestionText, question.QuestionText);
            var update = Builders<Question>.Update.SetOnInsert(doc => doc.QuestionText, question.QuestionText)
                .SetOnInsert(doc => doc.Category, question.Category)
                .SetOnInsert(doc => doc.CorrectAnswer, question.CorrectAnswer)
                .SetOnInsert(doc => doc.Difficulty, question.Difficulty)
                .SetOnInsert(doc => doc.IncorrectAnswers, question.IncorrectAnswers)
                .SetOnInsert(doc => doc.Type, question.Type);
            var upsertOne = new UpdateOneModel<Question>(filter, update) { IsUpsert = true };

            bulkOps.Add(upsertOne);
        }
        await _collection.BulkWriteAsync(bulkOps, cancellationToken: cancellationToken);
    }
}