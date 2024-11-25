using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CompositionApi;
[BsonIgnoreExtraElements]
public class GetAllCustomersModel {
    public string? Id { get; set; }
    public string? Firstname { get; set; }
    public string? Lastname { get; set; }

}