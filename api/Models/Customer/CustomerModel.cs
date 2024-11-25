using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CompositionApi;
public class CustomerModel {
    public string? Id { get; set; }
    public string? Firstname { get; set; }
    public string? Lastname { get; set; }
    public string? Adress { get; set; }
    public string? City { get; set; }
    public string? ZipCode { get; set; }

    public CustomerModel() {
        Id = ObjectId.GenerateNewId().ToString();
    }

}