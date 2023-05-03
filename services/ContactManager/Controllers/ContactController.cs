using System.Net;
using Microsoft.AspNetCore.Mvc;
using ContactManager.Services;
using ContactManager.Models;

namespace ContactManager.Controllers; 

/// <summary>
/// Contact service controller
/// </summary>
[ApiController]
[Route("[controller]")]
public class ContactController: ControllerBase {
    
    private readonly ContactService _contactService;
    private readonly ILogger<ContactController> _logger;
    /// <summary>
    /// Contact service controller
    /// </summary>
    public ContactController(ContactService contactService, ILogger<ContactController> logger) {
        _contactService = contactService;
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Get a list of all existing contacts
    /// </summary>
    /// <returns>A list of all contacts</returns>
    [HttpGet]
    [ProducesResponseType(typeof(List<Contact>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<List<Contact>>> Get() {
        return Ok(await _contactService.GetListAsync());
    }

    /// <summary>
    /// Creates a new contact
    /// </summary>
    /// <returns>New contact with Status code 201</returns>
    [HttpPost]
    [ProducesResponseType(typeof(Contact), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Post([FromBody] Contact contact) {
        await _contactService.CreateAsync(contact);
        return CreatedAtRoute("GetContact", new { id = contact.Id }, contact);
    }

    /// <summary>
    /// Updates and existing contact
    /// </summary>
    /// <returns>Updated contact with Status code 200</returns>
    [HttpPatch("{id:length(24)}", Name = "UpdateContact")]
    [ProducesResponseType(typeof(Contact), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Patch(string id, [FromBody] Contact contact) {
        return Ok(await _contactService.UpdateAsync(id, contact));
    }

    /// <summary>
    /// Flags/Unflags a contact as a favourite
    /// </summary>
    /// <returns>Status code 200</returns>
    [HttpPut("{id:length(24)}", Name = "FavaouriteContact")]
    [ProducesResponseType(typeof(Contact), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> AddContactToFavourites(string id) {
        await _contactService.AddContactToFavouritesAsync(id);
        return Ok();
    }

    /// <summary>
    /// Delete an existing contact
    /// </summary>
    /// <returns>Status code 204</returns>
    [HttpDelete("{id:length(24)}", Name = "DeleteContact")]
    [ProducesResponseType(typeof(Contact), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Delete(string id) {
        await _contactService.DeleteAsync(id);
        return NoContent();
    }

}