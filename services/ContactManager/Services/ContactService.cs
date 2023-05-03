using ContactManager.Models;
using ContactManager.Data;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;

namespace ContactManager.Services;

/// <summary>
/// Contact repository interface
/// </summary>
public interface IContactService {
    /// <summary>
    /// Get contact list service interface
    /// </summary>
    Task<List<Contact>> GetListAsync();
    /// <summary>
    /// Create contact service interface
    /// </summary>
    Task CreateAsync(Contact contact);
    /// <summary>
    /// Update contact service interface
    /// </summary>
    Task<bool> UpdateAsync(string id, Contact contact);
    /// <summary>
    /// Flag contact as favourite interface
    /// </summary>
    Task<bool> AddContactToFavouritesAsync(string id);
    /// <summary>
    /// Delete contact service interface
    /// </summary>
    Task<bool> DeleteAsync(string id);
}

/// <summary>
/// Service to manage contacts
/// </summary>
public class ContactService: IContactService {

    private readonly IMongoCollection<Contact> _contactCollection;

    /// <summary>
    /// Service to manage contacts
    /// </summary>
    public ContactService(IOptions<MongoDBSettings> mongoDBSettings) {
        MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
        IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
        _contactCollection = database.GetCollection<Contact>(mongoDBSettings.Value.CollectionName);
    }
    /// <summary>
    /// Get a list of all existing contacts
    /// </summary>
    /// <returns>A list of all contacts</returns>
    public async Task<List<Contact>> GetListAsync() {
        return await _contactCollection.Find(new BsonDocument()).SortBy(e => e.name).ToListAsync();
    }
    /// <summary>
    /// Creates a new contact
    /// </summary>
    public async Task CreateAsync(Contact contact) {
        await _contactCollection.InsertOneAsync(contact);
        return;
    }
    /// <summary>
    /// Updates and existing contact
    /// </summary>
    /// <returns>An updated contact</returns>
    public async Task<bool> UpdateAsync(string id, Contact contact) {
        FilterDefinition<Contact> filter = Builders<Contact>.Filter.Eq("Id", id);
        UpdateDefinition<Contact> update = Builders<Contact>.Update.Set<Contact>(c => c, contact).CurrentDate("lastModified");
        UpdateResult updateResult = await _contactCollection.UpdateOneAsync(filter, update);
        return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
    }
    /// <summary>
    /// Flags a contact as a favourite
    /// </summary>
    public async Task<bool> AddContactToFavouritesAsync(string id) {
        FilterDefinition<Contact> filter = Builders<Contact>.Filter.Eq("Id", id);
        Contact contact = await _contactCollection.Find(filter).SingleAsync();
        bool favourite = contact.favourite ? false : true;
        UpdateDefinition<Contact> update = Builders<Contact>.Update.Set<bool>("favourite", favourite).CurrentDate("lastModified");
        UpdateResult updateResult = await _contactCollection.UpdateOneAsync(filter, update);
        return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
    }
    /// <summary>
    /// Delete an existing contact
    /// </summary>
    public async Task<bool> DeleteAsync(string id) {
        FilterDefinition<Contact> filter = Builders<Contact>.Filter.Eq("Id", id);
        DeleteResult deleteResult = await _contactCollection.DeleteOneAsync(filter);
        return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
    }
}