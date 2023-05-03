using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace ContactManager.Models;

/// <summary>
/// Contact Manager Models
/// </summary>
public class Contact {
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    [BsonRequired]
    [BsonElement]
    public string name { get; set; } = null!;
    public string email { get; set; } = null!;
    public string phone { get; set; } = null!;
    public string company { get; set; } = null!;
    public string notes { get; set; } = null!;
    [BsonRepresentation(BsonType.Boolean)]
    public bool favourite { get; set; } = false!;
    public DateTime created { get; set; } = DateTime.Now!;
    public DateTime lastModified { get; set; } = DateTime.Now!;
}
