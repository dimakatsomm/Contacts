namespace ContactManager.Models;

/// <summary>
/// Service to manage contacts
/// </summary>
public class MongoDBSettings {
    public string ConnectionURI { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public string CollectionName { get; set; } = null!;

}