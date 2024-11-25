using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System.Linq;

namespace CompositionApi {
    public partial class MongoDbClient {

        public string mongoDbUrl = "mongodb://admin:isahedron@mongodb-service:27017/";
        MongoClient mongoDbClient = null!;

    //############################################################

    public MongoDbClient() {
        CreateDbConnection();
    }

    //############################################################

    public void CreateDbConnection() {
        mongoDbClient = new MongoClient(new MongoClientSettings {
            Server = new MongoServerAddress("mongodb-service", 27017),
            Credential = MongoCredential.CreateCredential("admin", "admin", "isahedron"),
            WriteConcern = WriteConcern.W1
        });
    }

    //############################################################

    public IMongoDatabase SelectDatabase(string dbName) {
        return mongoDbClient.GetDatabase(dbName);
    }

    //############################################################
    public void DropDatabase(string dbName) {
        mongoDbClient.DropDatabase(dbName);
    }

    //############################################################

    public IMongoCollection<BsonDocument> Collection(string dbName, string collectionName) {
        return SelectDatabase(dbName).GetCollection<BsonDocument>(collectionName);
    }

    //############################################################

    public IMongoCollection<T> TypedCollection<T>(string dbName, string collectionName) {
        return SelectDatabase(dbName).GetCollection<T>(collectionName);
    }

    //############################################################

    public IQueryable<T> QueryCollection<T>(string dbName, string collectionName) {
        return TypedCollection<T>(dbName, collectionName).AsQueryable();
    }

    //############################################################

    public void DropCollection(string dbName, string collectionName) {
        SelectDatabase(dbName).DropCollection(collectionName);
    }

    //############################################################

    public void InsertDocument<T>(string dbName, string collectionName, T document) {
        Collection(dbName, collectionName).InsertOne(document.ToBsonDocument());
    }

    //############################################################

    public void InsertDocuments<T>(string dbName, string collectionName, List<T> documents) {
        var bsonDocuments = documents.Select(doc => doc.ToBsonDocument()).ToList();
        Collection(dbName, collectionName).InsertMany(bsonDocuments);
    }

    //############################################################

    public T FindDocument<T>(string dbName, string collectionName, FilterDefinition<BsonDocument> filter) {
        return BsonSerializer.Deserialize<T>(Collection(dbName, collectionName).Find(filter).FirstOrDefault());
    }

    //############################################################

    public List<T> FindDocuments<T>(string dbName, string collectionName, FilterDefinition<BsonDocument> filter) {
        return Collection(dbName, collectionName).Find(filter).ToList().Select(doc => BsonSerializer.Deserialize<T>(doc)).ToList();
    }

    //############################################################

    public void UpdateField<T>(string dbName, string collectionName, FilterDefinition<BsonDocument> filter, string field, T value) {
        var update = Builders<BsonDocument>.Update.Set(field, value);
        Collection(dbName, collectionName).UpdateOne(filter, update);
    }

    //############################################################

    public void UpdateFields<T>(string dbName, string collectionName, FilterDefinition<BsonDocument> filter, Dictionary<string, T> fields) {
        var update = Builders<BsonDocument>.Update.Combine(fields.Select(field => Builders<BsonDocument>.Update.Set(field.Key, field.Value)));
        Collection(dbName, collectionName).UpdateOne(filter, update);
    }

    //############################################################

    public void ReplaceDocument<T>(string dbName, string collectionName, FilterDefinition<BsonDocument> filter, T replacement) {
        Collection(dbName, collectionName).ReplaceOne(filter, replacement.ToBsonDocument());
    }

    //############################################################

    public void ReplaceDocuments<T>(string dbName, string collectionName, FilterDefinition<BsonDocument> filter, List<T> replacements) {
        foreach (var replacement in replacements) {
            Collection(dbName, collectionName).ReplaceOne(filter, replacement.ToBsonDocument());
        }
    }

    //############################################################

    public void DeleteDocument(string dbName, string collectionName, FilterDefinition<BsonDocument> filter) {
        Collection(dbName, collectionName).DeleteOne(filter);
    }

    //############################################################

    public void DeleteDocuments(string dbName, string collectionName, FilterDefinition<BsonDocument> filter) {
        Collection(dbName, collectionName).DeleteMany(filter);
    }

    //############################################################

    }

}