using MongoDB.Bson.Serialization.Attributes;

namespace MicroserviceApp.Catalog.Api.Repositories;

public class BaseEntity
{
    //snowflakes
    [BsonElement("_id")] public Guid Id { get; set; }
}