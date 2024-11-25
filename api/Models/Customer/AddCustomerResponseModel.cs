using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CompositionApi;
[BsonIgnoreExtraElements]
public class AddCustomerResponseModel {
    public string? Status { get; set; }

}