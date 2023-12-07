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
}