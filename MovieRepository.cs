using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

[BsonIgnoreExtraElements]
class MovieSearchResult
{
    [BsonId]
    public ObjectId Id { get; set; }
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public string Genre { get; set; } = "";
    public double Score { get; set; }
}

class MovieRepository
{
    private readonly IMongoCollection<Movie> _collection;

    public MovieRepository(IMongoDatabase database)
    {
        _collection = database.GetCollection<Movie>("movies");
    }

    public async Task<bool> IsSeededAsync() =>
        await _collection.CountDocumentsAsync(Builders<Movie>.Filter.Empty) > 0;

    public async Task SeedAsync(IEnumerable<Movie> movies)
    {
        var list = movies.ToList();
        await _collection.InsertManyAsync(list);
        Console.WriteLine($"Seeded {list.Count} movies into MongoDB.");
    }

    public async Task EnsureVectorIndexAsync(int numDimensions = 768)
    {
        var indexDef = new BsonDocument("fields", new BsonArray
        {
            new BsonDocument
            {
                { "type", "vector" },
                { "path", "Embedding" },
                { "numDimensions", numDimensions },
                { "similarity", "cosine" }
            }
        });

        try
        {
            await _collection.SearchIndexes.CreateOneAsync(
                new CreateSearchIndexModel("vector_index", SearchIndexType.VectorSearch, indexDef));
        }
        catch (MongoCommandException ex) when (ex.Message.Contains("already exists"))
        {
            Console.WriteLine("Vector search index already exists.");
            return;
        }

        Console.Write("Building vector search index");
        while (true)
        {
            await Task.Delay(1000);
            Console.Write(".");
            try
            {
                using var cursor = await _collection.SearchIndexes.ListAsync();
                var indexes = await cursor.ToListAsync();
                var idx = indexes.FirstOrDefault(i => i["name"].AsString == "vector_index");
                if (idx != null && idx.GetValue("queryable", false).AsBoolean)
                    break;
            }
            catch (MongoCommandException)
            {
                await Task.Delay(9000);
                break;
            }
        }
        Console.WriteLine(" ready!");
    }

    public async Task<List<MovieSearchResult>> SearchAsync(float[] queryEmbedding, int limit = 5)
    {
        var queryVector = new BsonArray(queryEmbedding.Select(f => (BsonValue)(double)f));

        var pipeline = new BsonDocument[]
        {
            new("$vectorSearch", new BsonDocument
            {
                { "index", "vector_index" },
                { "path", "Embedding" },
                { "queryVector", queryVector },
                { "numCandidates", Math.Max(50, limit * 10) },
                { "limit", limit }
            }),
            new("$addFields", new BsonDocument("Score",
                new BsonDocument("$meta", "vectorSearchScore")))
        };

        return await _collection
            .Aggregate(PipelineDefinition<Movie, MovieSearchResult>.Create(pipeline))
            .ToListAsync();
    }
}
